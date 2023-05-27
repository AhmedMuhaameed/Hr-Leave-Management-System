using Blazored.LocalStorage;
using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Services.Base;

namespace Hr.LeaveManagement.BlazorUI.Services
{
    public class LeaveAllocationService : BaseHttpService, ILeaveAllocationService
    {
        public LeaveAllocationService(IClient client, ILocalStorageService localStorageService) : base(client, localStorageService)
        {
        }
        public async Task<Response<Guid>> CreateLeaveAllocations(int leaveTypeId)
        {
            try
            {
                await AddBearerToken();
                var response = new Response<Guid>();
                CreateLeaveAllocationCommand createLeaveAllocationCommand= new CreateLeaveAllocationCommand() { LeaveTypeId = leaveTypeId};

                await _client.LeaveAllocationPOSTAsync(createLeaveAllocationCommand);
                return response;
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions<Guid>(ex);
            }
        }
    }
}
