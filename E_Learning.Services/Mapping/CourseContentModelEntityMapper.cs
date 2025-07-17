
using AutoMapper;
using E_Learning.Domain.Entities;
using E_Learning.Services.Models;

namespace E_Learning.Services.Mapping
{
    /// <summary>
    /// CourseContentModelEntityMapper class for mapping between course content model and entity classes.
    /// </summary>
    public class CourseContentModelEntityMapper : Profile
    {
        public CourseContentModelEntityMapper()
        {
            // CourseContentModel to CourseContent entity mapping
            CreateMap<CourseContentModel, CourseContent>().ReverseMap();
        }
    }
}