using AutoMapper;
using Hr.LeaveManagement.BlazorUI.Models;
using Hr.LeaveManagement.BlazorUI.Models.LeaveAllocations;
using Hr.LeaveManagement.BlazorUI.Models.LeaveRequests;
using Hr.LeaveManagement.BlazorUI.Models.LeaveTypes;
using Hr.LeaveManagement.BlazorUI.Services.Base;

namespace Hr.LeaveManagement.BlazorUI.MappingProfiles
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<LeaveTypeDTO, LeaveTypeVM>().ReverseMap();
            CreateMap<LeaveTypeDetailDTO, LeaveTypeVM>().ReverseMap();
            CreateMap<CreateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();
            CreateMap<UpdateLeaveTypeCommand, LeaveTypeVM>().ReverseMap();

            CreateMap<LeaveRequestsDTO, LeaveRequestVM>()
               .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
               .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
               .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
               .ReverseMap();
            CreateMap<LeaveRequestDetailsDTO, LeaveRequestVM>()
                .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
                .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
                .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
                .ReverseMap();
            CreateMap<CreateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();
            CreateMap<UpdateLeaveRequestCommand, LeaveRequestVM>().ReverseMap();

            CreateMap<LeaveAllocationDTO, LeaveAllocationVM>().ReverseMap();
            CreateMap<LeaveAllocationDetailsDTO, LeaveAllocationVM>().ReverseMap();
            CreateMap<CreateLeaveAllocationCommand, LeaveAllocationVM>().ReverseMap();
            CreateMap<UpdateLeaveAllocationCommand, LeaveAllocationVM>().ReverseMap();


            CreateMap<EmployeeVM, Employee>().ReverseMap();
        }
    }
}
