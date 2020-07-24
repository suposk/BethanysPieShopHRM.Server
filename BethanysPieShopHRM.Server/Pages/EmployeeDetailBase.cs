using BethanysPieShopHRM.Server.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Pages
{
    public class EmployeeDetailBase : ComponentBase
    {
		[Parameter]
        public string EmployeeId { get; set; }

		[Parameter]
		public IEmployeeDataService EmployeeDataService { get; set; }

		public IEnumerable<Employee> Employees { get; set; }

        public Employee Employee { get; set; }

        private List<Country> Countries { get; set; }

		private List<JobCategory> JobCategories { get; set; }

		protected async override Task OnInitializedAsync()
		{
			Employee = await EmployeeDataService.GetEmployeeDetails(int.Parse(EmployeeId));			
		}
	}
}
