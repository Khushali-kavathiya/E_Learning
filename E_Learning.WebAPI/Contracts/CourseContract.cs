using E_Learning.Extensions.Attributes;
using E_Learning.Extensions.Mappings;
using E_Learning.Services.Models;

namespace E_Learning.WebAPI.Contracts;

/// <summary>
/// Request DTO for creating a course.
/// </summary>
public class CourseContract : IMapFrom<CourseModel>
{
    /// <summary>
    /// Title of the course.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Description of the course content.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Level of the course (e.g., Beginner, Intermediate, Advanced).
    /// </summary>
    public string? Level { get; set; }

    /// <summary>
    /// Duration of the course (e.g., "5 hours").
    /// </summary>
    public string? Duration { get; set; }

    /// <summary>
    /// Indicates whether the course is free.
    /// </summary>
    public bool IsFree { get; set; }

    /// <summary>
    /// Price of the course (required if not free).
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Date and time when the course was created.
    /// </summary>
    [CreateOnly]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}