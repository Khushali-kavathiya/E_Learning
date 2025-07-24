using E_Learning.Services.Models;   
using AutoMapper;
using E_Learning.WebAPI.Contracts;

namespace E_Learning.WebAPI.Mapping
{
    public class CommentContractModelMapper : Profile
    {
        public CommentContractModelMapper()
        {
            CreateMap<CommentContract, CommentModel>().ReverseMap();
        }
    }
}