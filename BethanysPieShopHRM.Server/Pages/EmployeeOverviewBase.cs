using BethanysPieShopHRM.Server.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.Server.Pages
{
    public class EmployeeOverviewBase : ComponentBase 
    {
		[Inject]
        public IEmployeeDataService EmployeeDataService { get; set; }


        [Inject]
        public ITestService TestService { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        protected async override Task OnInitializedAsync()
        {
            var test = TestService?.GetTime();

            Employees = await EmployeeDataService.GetAllEmployees();
        }

    }
}
