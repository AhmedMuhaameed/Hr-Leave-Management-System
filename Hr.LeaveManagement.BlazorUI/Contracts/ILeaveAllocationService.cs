using Hr.LeaveManagement.BlazorUI.Services.Base;

namespace Hr.LeaveManagement.BlazorUI.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId);
    }
}
