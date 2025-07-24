using AutoMapper;
using E_Learning.Domain.Entities;
using E_Learning.Services.Models;
namespace E_Learning.Services.Mapping
{
    public class CommentModelEntityMapper : Profile
    {
        public CommentModelEntityMapper()
        {
            CreateMap<Comment, CommentModel>().ReverseMap();
        }
    }
}