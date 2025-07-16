using AutoMapper;
using E_Learning.Domain.Entities;
using E_Learning.Services.Models;
using E_Learning.WebAPI.Contracts;

namespace E_Learning.WebAPI.Mapping;

/// <summary>
/// AutoMapper profile for mapping between <see cref="CourseContract"/> and <see cref="CourseModel"/>.
/// Handles transformation logic between API-layer DTOs and service-layer models for course-related operations.
/// </summary>
public class CourseContractModelMapper : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CourseContractModelMapper"/> class.
    /// Configures AutoMapper mappings for course creation and retrieval.
    /// </summary>
    public CourseContractModelMapper()
    {
        CreateMap<CourseContract, CourseModel>().ReverseMap();
        CreateMap<CourseContract, CourseModel>()
            .ForMember(dest => dest.InstructorId, opt => opt.Ignore());
    }
}