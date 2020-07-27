using BethanysPieShopHRM.Server.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Pages
{
    public class EmployeeEditBase : ComponentBase
    {
		[Parameter]
		public string EmployeeId { get; set; }

		[Inject]
		public IEmployeeDataService EmployeeDataService { get; set; }

		[Inject]
		public ICountryDataService CountryDataService { get; set; }

		[Inject]
        public IJobCategoryDataService JobCategoryDataService { get; set; }

		[Inject]
        public NavigationManager NavigationManager { get; set; }

        public Employee Employee { get; set; } = new Employee();

		public List<Country> Countries { get; set; } = new List<Country>();

		public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

		protected string CountryId = string.Empty;
		protected string JobCategoryId = string.Empty;
		protected string Message { get; set; }
		protected string Status { get; set; }
		protected bool Saved { get; set; }

        protected async override Task OnInitializedAsync()
		{
			Saved = false;
			Countries = (await CountryDataService.GetAllCountries()).ToList();

			JobCategories = (await JobCategoryDataService.GetAllJobCategories()).ToList();

			if (!string.IsNullOrWhiteSpace(EmployeeId))
				Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));
			else
				Employee = new Employee 
				{ 
					CountryId = 2, 
					JobCategoryId = 2, 
					BirthDate = DateTime.Now.AddYears(-30),   
					JoinedDate = DateTime.Now,
				};

			CountryId = Employee.CountryId.ToString();
			JobCategoryId = Employee.JobCategoryId.ToString();
		}

		public async Task DeleteHandle()
        {
			await EmployeeDataService.DeleteEmployee(Employee.EmployeeId);

			Status = "alert-success";
			Message = "Deleted";

			Saved = true;
		}

		public void GoBack() 
		{
			NavigationManager.NavigateTo("/employeeoverview");
		}

		protected async Task OnValidSubmitHandle()
        {
			Employee.CountryId = int.Parse(CountryId);
			Employee.JobCategoryId = int.Parse(JobCategoryId);

			if (Employee.EmployeeId == 0)
			{
				var res = await EmployeeDataService.AddEmployee(Employee);
				if (res != null)
                {
					Saved = true;
					Status = "alert-success";
					Message = "New Added";
                }
				else
                {
					Saved = false;
					Status = "alert-danger";
					Message = "Something went wrong";
				}
			}
			else
            {
				await EmployeeDataService.UpdateEmployee(Employee);
				Status = "alert-success";
				Message = "Updated";
				Saved = true;
			}				
		}

		protected void OnInvalidSubmitHandle()
		{
			Status = "alert-danger";
			Message = "Validation - Some validation errors, try again";
		}
	}
}

