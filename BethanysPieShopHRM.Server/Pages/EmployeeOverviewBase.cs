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
		[Parameter]
        public IEmployeeDataService EmployeeDataService { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Employees = await EmployeeDataService.GetAllEmployees();
        }

    }
}
