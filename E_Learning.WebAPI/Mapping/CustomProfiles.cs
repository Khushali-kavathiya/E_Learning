
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
        public CustomProfiles()
        {
            // Map UserModel to ApplicationUser using AutoMapper.
            CreateMap<UserModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();
        }
    }
}