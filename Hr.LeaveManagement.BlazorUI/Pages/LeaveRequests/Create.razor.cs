using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models.LeaveRequests;
using Hr.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;

namespace Hr.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Create
    {
        [Inject] ILeaveTypeService leaveTypeService { get; set; }
        [Inject] ILeaveRequestService leaveRequestService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        LeaveRequestVM LeaveRequest { get; set; } =new LeaveRequestVM();

        List<LeaveTypeVM> LeaveTypes { get; set; } = new List<LeaveTypeVM>();

        protected override async Task OnInitializedAsync()
        {
            LeaveTypes = await leaveTypeService.GetLeaveTypes();
        }
        private async Task HandleValidSubmit()
        {
            await leaveRequestService.CreateLeaveRequest(LeaveRequest);
            NavigationManager.NavigateTo("/leaverequests");
        }
    }
}