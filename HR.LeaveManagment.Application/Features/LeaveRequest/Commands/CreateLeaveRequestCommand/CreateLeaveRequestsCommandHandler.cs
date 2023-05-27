using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Contracts.Identity;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Models.Email;
using HR.LeaveManagment.Application.Contracts.Persistence;
using HR.LeaveManagment.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequestCommand
{
    public class CreateLeaveRequestsCommandHandler : IRequestHandler<CreateLeaveRequestCommand, Unit>
    {
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IUserService _userService;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public CreateLeaveRequestsCommandHandler(IEmailSender emailSender, IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository, IUserService userService, ILeaveAllocationRepository leaveAllocationRepository)
        {
            this._emailSender = emailSender;
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._leaveRequestRepository = leaveRequestRepository;
            this._userService = userService;
            this._leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<Unit> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
            var validationResults = await validator.ValidateAsync(request, cancellationToken);
            if (validationResults.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Request", validationResults);
            }
            //Get Employee Id
            var employeeId = _userService.UserId;

            //Check employee allocation
            var allocation = await _leaveAllocationRepository.GetUserAllocations(employeeId, request.LeaveTypeId);

            //if allocations aren't enough, return validation error with message
            if (allocation is null)
            {
                validationResults.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveTypeId), "You do not have any allocations for this leave type."));
                throw new BadRequestException("Invalid Leave Request", validationResults);
            }

            int daysRequested = (int)(request.EndDate - request.StartDate).TotalDays;
            if (daysRequested > allocation.NumberOfDays)
            {
                validationResults.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.EndDate), "You do not have enough days for this request."));
                throw new BadRequestException("Invalid Leave Request", validationResults);
            }

            //Create Leave Request
            var leaveRequest = _mapper.Map<Domain.LeaveRequest>(request);
            leaveRequest.RequestingEmployeeId = employeeId;
            leaveRequest.DateRequested = DateTime.Now;
            await _leaveRequestRepository.CreateAsync(leaveRequest);

            //send email confirmation
            try
            {
                var email = new EmailMessage
                {
                    To = string.Empty,
                    Body = $"Your leave request for {request.StartDate:D} to {request.EndDate:D} " + $"has beed submitted successfully.",
                    Subject = "Leave Request submitted",
                };
                await _emailSender.SendEmail(email);

            }
            catch(Exception ex)
            {
                // Log or handle error
            }
          
            return Unit.Value;
        }
    }
}
