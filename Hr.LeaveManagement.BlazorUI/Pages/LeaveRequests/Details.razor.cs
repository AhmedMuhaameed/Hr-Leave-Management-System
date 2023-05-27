using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models.LeaveRequests;
using Microsoft.AspNetCore.Components;

namespace Hr.LeaveManagement.BlazorUI.Pages.LeaveRequests
{
    public partial class Details
    {
        [Inject] ILeaveRequestService leaveRequestService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        [Parameter] public int id { get; set; }
        string ClassName;
        string HeadingText;
        public LeaveRequestVM Model { get; private set; } = new LeaveRequestVM();

        protected async override Task OnParametersSetAsync()
        {
            Model = await leaveRequestService.GetLeaveRequestAsync(id);
        }
        protected override async Task OnInitializedAsync()
        {
            if(Model.Approved == null)
            {
                ClassName = "warning";
                HeadingText = "Pending Approval";
            }else if(Model.Approved == true)
            {
                ClassName = "success";
                HeadingText = "Approved";
            }
            else
            {
                ClassName = "danger";
                HeadingText = "Rejected";
            }
        }
        async Task ChangeApproval(bool approvalStatus)
        {
            await leaveRequestService.ApproveLeaveRequest(id, approvalStatus);
            NavigationManager.NavigateTo("/leaverequests/");
        }

    }
}