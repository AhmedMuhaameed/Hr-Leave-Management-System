using AutoMapper;
using FluentValidation.Results;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequestCommand;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagment.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequestCommand
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _appLogger;

        public UpdateLeaveRequestCommandHandler(IEmailSender emailSender, IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository, IAppLogger<UpdateLeaveRequestCommandHandler> appLogger)
        {
            this._emailSender = emailSender;
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._leaveRequestRepository = leaveRequestRepository;
            this._appLogger = appLogger;
        }
        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
            if (leaveRequest is null)
            {
                throw new NotFoundException(nameof(leaveRequest), request.Id);
            }

            var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);
            var validationResults = await validator.ValidateAsync(request, cancellationToken);
            if (validationResults.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Request", validationResults);
            }
            _mapper.Map(request, leaveRequest);

            await _leaveRequestRepository.UpdateAsync(leaveRequest);

            try
            {

                var email = new EmailMessage
                {
                    To = string.Empty,
                    Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " + $"has beed updated successfully.",
                    Subject = "Leave Request submitted",
                };
                await _emailSender.SendEmail(email);
            }
            catch (Exception ex)
            {
                _appLogger.LogWarnings(ex.Message);
            }

            return Unit.Value;
        }
    }
}
