using E_Learning.Services.Models;
using E_Learning.WebAPI.Contracts;
using AutoMapper;

namespace E_Learning.WebAPI.Mapping
{
    public class CourseContentContractModelMapper : Profile
    {
        public CourseContentContractModelMapper()
        {
            CreateMap<CourseContentContract, CourseContentModel>().ReverseMap();
        }
    }
}