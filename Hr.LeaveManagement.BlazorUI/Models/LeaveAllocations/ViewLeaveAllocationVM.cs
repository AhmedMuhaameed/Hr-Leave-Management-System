namespace Hr.LeaveManagement.BlazorUI.Models.LeaveAllocations
{
    public class ViewLeaveAllocationVM
    {
        public string EmployeeId { get; set; }
        public List<LeaveAllocationVM> LeaveAllocations { get; set; }
    }
}
