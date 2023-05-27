using Blazored.Toast.Services;
using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;
using System.Xml.Serialization;

namespace Hr.LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Index
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public ILeaveTypeService LeaveTypeService { get; set; }
        [Inject]
        public ILeaveAllocationService LeaveAllocationService { get; set; }
        [Inject] IToastService toastService { get; set; }
        public List<LeaveTypeVM> LeaveTypes { get; set; }
        public string Message { get; set; } = string.Empty;

        protected void CreateLeaveType()
        {
            NavigationManager.NavigateTo("/leavetypes/create/");
        }
        protected async void AllocateLeaveType(int id)
        {
            await LeaveAllocationService.CreateLeaveAllocations(id);

        }
        protected void EditLeaveType(int id) {
            NavigationManager.NavigateTo($"/leavetypes/edit/{id}");
        }
        protected void DetailsLeaveType(int id)
        {
            NavigationManager.NavigateTo($"/leavetypes/details/{id}");
        }
        protected async void DeleteLeaveType(int id)
        {
            var response = await LeaveTypeService.DeleteLeaveType(id);
            if (response.Success)
            {
                toastService.ShowSuccess("Leave Type deleted successfully");
                await OnInitializedAsync();
            }
            else
            {
                Message = response.Message;
            }
        }
     
        protected override async Task OnInitializedAsync()
        {
            LeaveTypes = await LeaveTypeService.GetLeaveTypes();
        }
    }
}