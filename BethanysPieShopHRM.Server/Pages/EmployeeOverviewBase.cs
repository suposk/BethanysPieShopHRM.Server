using BethanysPieShopHRM.Server.Components;
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


        public AddEmployeeDialog AddEmployeeDialog { get; set; }

        [Inject]
        public ITestService TestService { get; set; }

        public List<Employee> Employees { get; set; }

        protected override Task OnInitializedAsync()
        {
            var test = TestService?.GetTime();
            return LoadAll();
        }

        private async Task LoadAll()
        {
            Employees = (await EmployeeDataService.GetAllEmployees()).ToList();
        }

        public void QuickAddEmployee()
        {
            AddEmployeeDialog.Show();
        }

        public async void OnClosedDialogEvent(Employee added)
        {
            if (added != null && added.EmployeeId > 0)
            {
                Employees.Add(added);
            }
            else
                await LoadAll();
            StateHasChanged();
        }

    }
}
