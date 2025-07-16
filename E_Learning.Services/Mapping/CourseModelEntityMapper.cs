using AutoMapper;
using E_Learning.Domain.Entities;
using E_Learning.Services.Models;

namespace E_Learning.Services.Mapping;

/// <summary>   
/// default AutoMapper profile for mapping between course model and entity classes. 
/// </summary>
public class CourseModelEntityMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CourseModelEntityMapper"/> class.
    /// </summary>
    public CourseModelEntityMapper()
    {
        // Mapping between CourseModel and Course entity
        CreateMap<CourseModel, Course>()
        .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

        CreateMap<Course, CourseModel>();
    }
}