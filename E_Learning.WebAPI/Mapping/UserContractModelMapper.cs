using E_Learning.WebAPI.Contracts;
using E_Learning.Services.Models;
using E_Learning.Domain.Enums;
using AutoMapper;

namespace E_Learning.WebAPI.Mapping;

/// <summary>
/// AutoMapper profile for mapping between <see cref="UserContract"/> and <see cref="UserModel"/>.
/// This handles conversion logic between API contracts and internal service models used in authentication and user operations.
/// </summary>
public class UserContractModelMapperProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserContractModelMapperProfile"/> class.
    /// Configures AutoMapper to map between <see cref="UserContract"/> and <see cref="UserModel"/> bidirectionally.
    /// </summary>
    public UserContractModelMapperProfile()
    {
        // Mapping between RegisterUserContract and RegisterUserModel.
        CreateMap<UserContract, UserModel>().ReverseMap();
    }
}



