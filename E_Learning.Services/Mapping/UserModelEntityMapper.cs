using E_Learning.Domain.Entities;
using E_Learning.Services.Models;
using AutoMapper;

namespace E_Learning.Services.Mapping;

/// <summary>
/// AutoMapper profile for mapping between user model and entity classes.
/// </summary>
public class UserModelEntityMapper : Profile
{
    public UserModelEntityMapper()
    {
        // Mapping between RegisterUserModel and ApplicationUser
        // Maps the Email property of RegisterUserModel to the UserName property of ApplicationUser

        CreateMap<RegisterUserModel, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

        // Mapping between ApplicationUser and UserProfileModel    
        CreateMap<ApplicationUser, UserProfileModel>();   
    }
}
