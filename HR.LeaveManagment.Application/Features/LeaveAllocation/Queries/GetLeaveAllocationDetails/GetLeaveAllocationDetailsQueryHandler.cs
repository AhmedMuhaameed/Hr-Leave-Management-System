using AutoMapper;
using HR.LeaveManagment.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
    public class GetLeaveAllocationDetailsQueryHandler : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDTO>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public GetLeaveAllocationDetailsQueryHandler(IMapper mapper,ILeaveAllocationRepository leaveAllocationRepository)
        {
            this._mapper = mapper;
            this._leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<LeaveAllocationDetailsDTO> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
        {
            var record = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);
            var leaveAllocation = _mapper.Map<LeaveAllocationDetailsDTO>(record);
            return leaveAllocation;
        }
    }
}
