using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class GetLeaveTypeDetailsQueryHandler : IRequestHandler<GetLeaveTypeDetailsQuery, LeaveTypeDetailDTO>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public GetLeaveTypeDetailsQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<LeaveTypeDetailDTO> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
        {
            var leaveTypeDetails = await _leaveTypeRepository.GetByIdAsync(request.Id);

            if (leaveTypeDetails == null)
            {
                throw new NotFoundException(nameof(LeaveType), request.Id);
            }
            var data = _mapper.Map<LeaveTypeDetailDTO>(leaveTypeDetails);

            return data;
        }
    }
}
