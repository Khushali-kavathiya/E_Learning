using System.ComponentModel.DataAnnotations;
using E_Learning.Domain.Enums;
using E_Learning.Extensions.Attributes;
using E_Learning.Extensions.Mappings;
using E_Learning.Services.Models;

namespace E_Learning.WebAPI.Contracts
{
    /// <summary>
    /// Contains information about a course content.
    /// </summary>
    public class CourseContentContract : IMapFrom<CourseContentModel>, IValidatableObject
    {
        /// <summary>
        /// CourseId of the course this content belongs to.
        /// </summary>
        [CreateOnly]
        [Required(ErrorMessage = "Course ID is required")]
        public Guid CourseId { get; set; }

        /// <summary>
        /// Get or set the title of the content.
        /// </summary>
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
        public string? Title { get; set; }

        /// <summary>
        /// Get or set the description of the content.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Get or set the content URL.
        /// </summary>
        [Required(ErrorMessage = "Content URL is required")]
        public string? ContentUrl { get; set; }

        /// <summary>
        /// Content type of the content(e.g., Video, Article, PDF, Quiz).
        /// </summary>
        [Required(ErrorMessage = "Content type is required")]
        [EnumDataType(typeof(CourseContentType))]
        public CourseContentType ContentType { get; set; }

        /// <summary>
        /// Get or set the order of the content in the course.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Order must be a positive integer")]
        public int Order { get; set; }

        /// <summary>
        /// Custom validation to ensure the content URL is a valid absolute URL.
        /// </summary>
        /// <param name="context">The validation context.</param>
        /// <returns>An IEnumerable of validation results.</returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            if (!string.IsNullOrWhiteSpace(ContentUrl) &&
               (!Uri.TryCreate(ContentUrl, UriKind.Absolute, out _)))
            {
                yield return new ValidationResult("Content URL must be a valid absolute URL", new[] { nameof(ContentUrl) });
            }
        }
    }
}