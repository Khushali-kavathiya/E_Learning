using E_Learning.Domain.Enums;
using E_Learning.Extensions.Attributes;
using E_Learning.Extensions.Mappings;
using E_Learning.Services.Models;

namespace E_Learning.WebAPI.Contracts
{
    /// <summary>
    /// Contains information about a course content.
    /// </summary>
    public class CourseContentContract : IMapFrom<CourseContentModel>
    {
        /// <summary>
        /// CourseId of the course this content belongs to.
        /// </summary>
        [CreateOnly]
        public Guid CourseId { get; set; }

        /// <summary>
        /// Get or set the title of the content.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// Get or set the description of the content.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Get or set the content URL.
        /// </summary>
        public string? ContentUrl { get; set; }

        /// <summary>
        /// Content type of the content(e.g., Video, Article, PDF, Quiz).
        /// </summary>
        public CourseContentType ContentType { get; set; }

        /// <summary>
        /// Get or set the order of the content in the course.
        /// </summary>
        public int Order { get; set; }
    }
}