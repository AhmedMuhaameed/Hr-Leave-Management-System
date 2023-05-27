using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestesQuery
{
    public class GetLeaveRequestsQuery :IRequest<List<LeaveRequestsDTO>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
