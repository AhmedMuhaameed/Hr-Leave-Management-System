using AutoMapper;
using HR.LeaveManagment.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
    public class GetLeaveAllocationsQueryHandler : IRequestHandler<GetLeaveAllocationQuery, List<LeaveAllocationDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public GetLeaveAllocationsQueryHandler(IMapper mapper,ILeaveAllocationRepository leaveAllocationRepository)
        {
            this._mapper = mapper;
            this._leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<List<LeaveAllocationDTO>> Handle(GetLeaveAllocationQuery request, CancellationToken cancellationToken)
        {
            var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();
            var allocations = _mapper.Map<List<LeaveAllocationDTO>>(leaveAllocations);
            return allocations;
        }
    }
}
