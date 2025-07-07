using E_Learning.Domain.Entities;
using E_Learning.Services.Models;
using AutoMapper;

namespace E_Learning.Services.Mapping;

public class UserModelEntityMapper : Profile
{
    public UserModelEntityMapper()
    {
        CreateMap<RegisterUserModel, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    }
}
