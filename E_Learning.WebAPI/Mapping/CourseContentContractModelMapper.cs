using E_Learning.Services.Models;
using E_Learning.WebAPI.Contracts;
using AutoMapper;

namespace E_Learning.WebAPI.Mapping
{
    /// <summary>
    /// AutoMapper profile for mapping between course content contract and model.
    /// </summary>
    public class CourseContentContractModelMapper : Profile
    {
        /// Initializes a new instance of the <see cref="CourseContentContractModelMapper"/> class.
        public CourseContentContractModelMapper()
        {
            CreateMap<CourseContentContract, CourseContentModel>().ReverseMap();
        }
    }
}   