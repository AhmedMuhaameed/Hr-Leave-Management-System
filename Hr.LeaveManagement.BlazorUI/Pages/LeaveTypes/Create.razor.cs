using Blazored.Toast.Services;
using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;
using System.Xml.Serialization;

namespace Hr.LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Create
    {
        [Inject]
        NavigationManager _navManager { get; set; }
        [Inject]
        ILeaveTypeService _client { get; set; }
        [Inject]
        IToastService toastService { get; set; }
        public string Message { get; private set; }

        LeaveTypeVM leaveType = new LeaveTypeVM();
        async Task CreateLeaveType()
        {
            var response = await _client.CreateLeaveType(leaveType);
            if (response.Success)
            {
                toastService.ShowSuccess("Leave Type Created Successfully");
                _navManager.NavigateTo("/leavetypes/");
            }
            Message = response.Message;
        }
    }
}