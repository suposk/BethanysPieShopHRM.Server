using BethanysPieShopHRM.Server.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Components
{
    public class AddEmployeeDialogBase : ComponentBase
    {
		[Inject]
		public IEmployeeDataService EmployeeDataService { get; set; }

		[Parameter]
		public EventCallback<Employee> ClosedDialogEvent { get; set; }

        public Employee Employee { get; set; } = CreateNew();

		protected string Message { get; set; }
		protected string Status { get; set; }
		protected bool Saved { get; set; }
        public bool ShowDialog { get; set; }

		public void Show()
        {
			Employee = CreateNew();
			ShowDialog = true;
			StateHasChanged();
        }

		public void Close()
        {
			ShowDialog = false;			
			StateHasChanged();			
        }

		public static Employee CreateNew() => new Employee
		{
			CountryId = 2,
			JobCategoryId = 2,
			BirthDate = DateTime.Now.AddYears(-30),
			JoinedDate = DateTime.Now,
		};

		protected async Task OnValidSubmitHandle()
		{
			var res = await EmployeeDataService.AddEmployee(Employee);
			if (res != null)
			{
				ShowDialog = false;
				Saved = true;
				Status = "alert-success";
				Message = "New Added";
				await ClosedDialogEvent.InvokeAsync(res);
			}
			else
			{
				Saved = false;
				Status = "alert-danger";
				Message = "Something went wrong";
			}
			StateHasChanged();
		}

		protected void OnInvalidSubmitHandle()
		{
			Status = "alert-danger";
			Message = "Validation - Some validation errors, try again";
		}
	}

}
