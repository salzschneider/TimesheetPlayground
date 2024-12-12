using AutoMapper;
using System;
using TimesheetPlayground.DAL.Entities;
using TimesheetPlayground.UI.Models.DTO;
using TimesheetPlayground.UI.Models.Enum;
using TimesheetPlayground.UI.Models;

namespace TimesheetPlayground.UI.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDO>().
                ForMember(nameof(UserDO.Role), (x) => x.MapFrom(src => (RoleEnum)Enum.Parse(typeof(RoleEnum), src.Role)));
            CreateMap<UserDO, User>().
                ForMember(nameof(User.Role), (x) => x.MapFrom<string>(r => r.Role.ToString()));

            CreateMap<Project, ProjectDO>();
            CreateMap<ProjectDO, Project>();

            CreateMap<Timesheet, TimesheetDO>().
                ForMember(nameof(TimesheetDO.Status), (x) => x.MapFrom(src => (StatusEnum)Enum.Parse(typeof(StatusEnum), src.Status)));
            CreateMap<TimesheetDO, Timesheet>().
                ForMember(nameof(Timesheet.Status), (x) => x.MapFrom<string>(r => r.Status.ToString()));

            CreateMap<WorkedHour, WorkedHourDO>();
            CreateMap<WorkedHourDO, WorkedHour>();

            CreateMap<ProjectViewModel, ProjectDO>();
        }
    }
}
