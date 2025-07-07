using E_Learning.WebAPI.Contracts;
using E_Learning.Services.Models;
using AutoMapper;

namespace E_Learning.WebAPI.Mapping;

public class AuthContractModelMapperProfile : Profile
{
    public AuthContractModelMapperProfile()
    {
        CreateMap<RegisterUserContract, RegisterUserModel>();
        CreateMap<LoginRequest, LoginRequestModel>();      
    }
}



