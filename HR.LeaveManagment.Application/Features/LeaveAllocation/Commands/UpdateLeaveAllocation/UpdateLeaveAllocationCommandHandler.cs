using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagment.Application.Contracts.Persistence;
using HR.LeaveManagment.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationRepository ,ILeaveTypeRepository leaveTypeRepository)
        {
            this._mapper = mapper;
            this._leaveAllocationRepository = leaveAllocationRepository;
            this._leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllocationValidator(_leaveAllocationRepository,_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any()) {
                throw new BadRequestException("Invalid Leave Allocation Request", validationResult);
            }
            var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

            if(leaveAllocation is null)
            {
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);
            }

            _mapper.Map(request, cancellationToken);

            await _leaveAllocationRepository.UpdateAsync(leaveAllocation);

            return Unit.Value;
        }
    }
}
