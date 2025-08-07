using System.ComponentModel.DataAnnotations;
using E_Learning.Extensions.Attributes;
using E_Learning.Extensions.Mappings;
using E_Learning.Services.Models;

namespace E_Learning.WebAPI.Contracts;

/// <summary>
/// Request DTO for creating a course.
/// </summary>
public class CourseContract : IMapFrom<CourseModel>, IValidatableObject
{
    /// <summary>
    /// Title of the course.
    /// </summary>
    [Required(ErrorMessage = "Course title is required")]
    [MaxLength(100, ErrorMessage = "Title cannot exceed 100 characters")]
    public string? Title { get; set; }

    /// <summary>
    /// Description of the course content.
    /// </summary>
    [Required(ErrorMessage = "Description is required")]
    public string? Description { get; set; }

    /// <summary>
    /// Level of the course (e.g., Beginner, Intermediate, Advanced).
    /// </summary>
    [Required(ErrorMessage = "Course level is required")]
    [RegularExpression("^(Beginner|Intermediate|Advanced)$", ErrorMessage = "Level must be one of: Beginner, Intermediate, Advanced")]
    public string? Level { get; set; }

    /// <summary>
    /// Duration of the course (e.g., "5 hours").
    /// </summary>
    [Required(ErrorMessage = "Duration is required")]
    public string? Duration { get; set; }

    /// <summary>
    /// Indicates whether the course is free.
    /// </summary>
    public bool IsFree { get; set; }

    /// <summary>
    /// Price of the course (required if not free).
    /// </summary>
    [Range(0, double.MaxValue, ErrorMessage = "Price must be zero or greater if course is not free")]
    public decimal Price { get; set; }

    /// <summary>
    /// Date and time when the course was created.
    /// </summary>
    [CreateOnly]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    /// <summary>
    /// Custom Validation logic (e.g., price check based on IsFree).
    /// </summary>
    /// <param name="context">Validation context</param>
    /// <returns>Validation results</returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext context)
    {
        if (!IsFree && Price <= 0)
        {
            yield return new ValidationResult("Price must be greater than zero if course is not free", new[] { nameof(Price) });
        }
    }
}