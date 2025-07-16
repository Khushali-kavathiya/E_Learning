using E_Learning.Domain.Enums;
namespace E_Learning.Domain.Entities;

public class CourseContent
{
    /// <summary>
    /// Primary key
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Title of the content (e.g., "Introduction", "Lesson 1").
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Description or notes about the content.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// URL to access the video, document, or material.
    /// </summary>
    public string ContentUrl { get; set; }

    /// <summary>
    /// Type of content (e.g., Video, Article, PDF, Quiz).
    /// </summary>
    public CourseContentType ContentType { get; set; }

    /// <summary>
    /// Foreign key to the course.
    /// </summary>
    public Guid CourseId { get; set; }

    /// <summary>
    ///  Navigation property to the related course.
    /// </summary>
    public Course Course { get; set; }

    /// <summary>
    /// Order of content within the course.
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// UTC timestamp when the content was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// UTC timestamp when the content was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }
}