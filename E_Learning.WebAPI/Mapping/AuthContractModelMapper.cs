using E_Learning.WebAPI.Contracts;
using E_Learning.Services.Models;
using AutoMapper;

namespace E_Learning.WebAPI.Mapping;

/// <summary>
/// AutoMapper profile for mapping between contract and model classes related to authentication.
/// </summary>
public class AuthContractModelMapperProfile : Profile
{
    public AuthContractModelMapperProfile()
    {
        // Mapping between RegisterUserContract and RegisterUserModel
        CreateMap<RegisterUserContract, RegisterUserModel>();

        // Mapping between LoginRequest and LoginRequestModel
        CreateMap<LoginRequest, LoginRequestModel>();

        // Mapping between UserProfileModel and UserProfileResponse
        CreateMap<UserProfileModel, UserProfileResponse>();
    }
}



