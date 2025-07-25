using E_Learning.Domain.Entities;
using E_Learning.Domain.Enums;
using E_Learning.Extensions.Mappings;

namespace E_Learning.Services.Models
{
    /// <summary>
    /// Corresponds to a course content item within a course.
    /// </summary>
    public class CourseContentModel : IMapFrom<CourseContent>
    {
        /// <summary>
        /// Get or sets the unique identifier of the course content item.
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Get or sets the title of the course content item.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Get or sets the description of the course content item.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Get or sets the URL of the content item.
        /// </summary>
        public string ContentUrl { get; set; }

        /// <summary>
        /// Get or sets the type of content item (e.g., Video, Article, PDF, Quiz).
        /// </summary>
        public CourseContentType ContentType { get; set; }

        /// <summary>
        /// Get or sets the order of the content item within the course.
        /// </summary>
        public int Order { get; set; }
    }
}