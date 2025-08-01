
using AutoMapper;
using E_Learning.Domain.Entities;
using E_Learning.Services.Models;

namespace E_Learning.WebAPI.Mapping
{
    /// <summary>
    /// custom profile for mapping.
    /// </summary>
    public class CustomProfiles : Profile
    {
        /// Initializes a new instance of the <see cref="CustomProfiles"/> class.
        public CustomProfiles()
        {
            // Map UserModel to ApplicationUser using AutoMapper.
            CreateMap<UserModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();

            // Map Course to InstructorDashboardCourseModel using AutoMapper.
            CreateMap<Course, InstructorDashboardCourseModel>()
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.Id));

            // Map Course to StudentDashboardCourseModel using AutoMapper.
            CreateMap<Course, StudentDashboardCourseModel>()
               .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.InstructorName, opt => opt.MapFrom(src => src.Instructor.FullName));

        }
    }
}