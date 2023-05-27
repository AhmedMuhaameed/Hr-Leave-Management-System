using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestesQuery
{
    public class GetLeaveRequestsQueryHandler : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestsDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IUserService _userService;

        public GetLeaveRequestsQueryHandler(IMapper mapper,ILeaveRequestRepository leaveRequestRepository,IUserService userService)
        {
            this._mapper = mapper;
            this._leaveRequestRepository = leaveRequestRepository;
            this._userService = userService;
        }
        public async Task<List<LeaveRequestsDTO>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
        {
            var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
            var requests = _mapper.Map<List<LeaveRequestsDTO>>(leaveRequests);

            //check if it is loggedin employeee
            if (request.IsLoggedInUser)
            {
                var userId = _userService.UserId;
                leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails(userId);

                var employee = await _userService.GetEmployee(userId);
                requests = _mapper.Map<List<LeaveRequestsDTO>>(leaveRequests);
                foreach(var req in requests)
                {
                    req.Employee = employee;
                }

            }
            else
            {
                leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
                requests =  _mapper.Map<List<LeaveRequestsDTO>>(leaveRequests);
                foreach(var req in requests)
                {
                    req.Employee = await _userService.GetEmployee(req.RequestingEmployeeId);
                }
            }
            return requests;
        }
    }
}
