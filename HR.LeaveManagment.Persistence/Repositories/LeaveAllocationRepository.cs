using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagment.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
    {
        public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task AddAllocations(List<LeaveAllocation> allocations)
        {
            await _context.AddRangeAsync(allocations);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> AllocationExisits(string userId, int leaveTypeId, int period)
        {
            return await _context.LeaveAllocations.AnyAsync(i => i.EmployeeId == userId && i.LeaveTypeId == leaveTypeId && i.Period == period);
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var leaveAllocations = await _context.LeaveAllocations.Include(i => i.LeaveType).ToListAsync();
            return leaveAllocations;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId)
        {
            var leaveAllocations = await _context.LeaveAllocations.Where(i => i.EmployeeId == userId).Include(i => i.LeaveType).ToListAsync();
            return leaveAllocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var leaveAllocations = await _context.LeaveAllocations.Include(i => i.LeaveType).FirstOrDefaultAsync(i => i.Id == id);
            return leaveAllocations;
        }
        public async Task<LeaveAllocation> GetUserAllocations(string userId, int leaveTypeId)
        {
            return await _context.LeaveAllocations.FirstOrDefaultAsync(i => i.EmployeeId == userId && i.LeaveTypeId == leaveTypeId);
        }
    }
}
