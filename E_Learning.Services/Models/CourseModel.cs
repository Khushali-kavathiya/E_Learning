namespace E_Learning.Services.Models;

/// <summary>
/// Business model for creating a course.
/// </summary>
public class CourseModel
{
    /// <summary>
    /// Title of the course.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Description of the course content.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Level of the course(e.g., Beginner, Intermediate, Advanced).
    /// </summary>
    public string Level { get; set; }

    /// <summary>
    /// Duration of the course(e.g., "5 hours").
    /// </summary>
    public string Duration { get; set; }

    /// <summary>
    /// Indicates whether the course is free.
    /// </summary>
    public bool IsFree { get; set; }

    /// <summary>
    /// Price of the courses(if not free).
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Foreign key for the instructor (ApplicationUser).
    /// </summary>
    public string InstructorId { get; set; }

    /// <summary>
    /// Date and time when the course was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

}