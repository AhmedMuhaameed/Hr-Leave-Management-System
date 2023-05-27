using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace Hr.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Index
    {
        [Inject] ILeaveRequestService leaveRequestService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        public AdminLeaveRequestViewVM Model { get; set; } = new();

        protected async override Task OnInitializedAsync()
        {
            Model = await leaveRequestService.GetAdminLeaveRequestsList();
        }

        void GoToDetails(int id)
        {
            NavigationManager.NavigateTo($"/leaverequests/details/{id}");
        }
    }
}