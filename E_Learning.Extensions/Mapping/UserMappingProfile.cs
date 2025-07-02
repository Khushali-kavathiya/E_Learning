using AutoMapper;
using E_Learning.Services.Models;
using E_Learning.Domain.Entities;


namespace E_Learning.Extensions.Mapping;

/// <summary>
/// AutoMapper Profile for user-related mappings.
/// </summary>
public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterRequest, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.FullName));

        CreateMap<ApplicationUser, UserResponse>()
            .ForMember(dest => dest.Roles, opt => opt.Ignore()); //// Roles fetched separately
    }
}