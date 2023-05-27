using Hr.LeaveManagement.BlazorUI.Models.LeaveRequests;
using Hr.LeaveManagement.BlazorUI.Services.Base;

namespace Hr.LeaveManagement.BlazorUI.Contracts
{
    public interface ILeaveRequestService
    {
        Task<Response<Guid>> CreateLeaveRequest(LeaveRequestVM leaveRequest);
        Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestsList();
        Task<LeaveRequestVM> GetLeaveRequestAsync(int id);
        Task ApproveLeaveRequest(int id, bool approved);
        Task<EmployeeLeaveRequestViewVM> GetUserLeaveRequests();
        Task<Response<Guid>> CancelLeaveRequest(int id);
    }
}
