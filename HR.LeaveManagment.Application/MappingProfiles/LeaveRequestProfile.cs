using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequestCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequestCommand;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetailsQuery;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestesQuery;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveRequestProfile :Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<LeaveRequestsDTO, LeaveRequest>().ReverseMap();
            CreateMap<LeaveRequestDetailsDTO, LeaveRequest>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestDetailsDTO>();
            CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
            CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
        }
    }
}
