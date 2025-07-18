using E_Learning.Services.Models;
using E_Learning.Domain.Entities;
using AutoMapper;

namespace E_Learning.Services.Mapping
{
    /// <summary>
    /// AutoMapper profile for mapping between enrollment model and entity classes.
    /// </summary>
    public class EnrollmentModelEntityMapper : Profile
    {
        /// Initializes a new instance of the <see cref="EnrollmentModelEntityMapper"/> class.
        public EnrollmentModelEntityMapper()
        {
            CreateMap<Enrollment, EnrollmentModel>().ReverseMap();
        }
    }
}