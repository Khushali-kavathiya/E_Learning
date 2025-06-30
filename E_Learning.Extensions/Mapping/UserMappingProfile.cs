using AutoMapper;
using E_Learning.Services.Models;
using E_Learning.Domain.Entities;


namespace E_Learning.Extensions.Mapping;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterRequest, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    }
}