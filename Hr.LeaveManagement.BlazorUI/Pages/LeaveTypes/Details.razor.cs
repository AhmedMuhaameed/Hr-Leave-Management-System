using Hr.LeaveManagement.BlazorUI.Contracts;
using Hr.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Microsoft.AspNetCore.Components;
using System.Xml.Serialization;

namespace Hr.LeaveManagement.BlazorUI.Pages.LeaveTypes
{
    public partial class Details
    {
        [Inject]
        ILeaveTypeService _client { get; set; }

        [Parameter]
        public int id { get; set; }

        LeaveTypeVM leaveType = new LeaveTypeVM();

        protected async override Task OnParametersSetAsync()
        {
            leaveType = await _client.GetLeaveType(id);
        }
    }
}