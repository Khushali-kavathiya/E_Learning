using E_Learning.WebAPI.Contracts;
using E_Learning.Services.Models;
using AutoMapper;

namespace E_Learning.WebAPI.Mapping
{
    /// <summary>
    /// AutoMapper profile for mapping between enrollment contract and model.
    /// </summary>
    public class EnrollmentContractModelMapper : Profile
    {
        /// Initializes a new instance of the <see cref="EnrollmentContractModelMapper"/> class.
        public EnrollmentContractModelMapper()
        {
            CreateMap<EnrollmentContract, EnrollmentModel>().ReverseMap();
        }
    }
}