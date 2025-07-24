namespace E_Learning.Domain.Entities;

/// <summary>
/// Represents a comment made by a user on a course.
/// </summary>
public class Comment
{
    /// <summary>
    /// Unique identifier of the comment.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Course id on which the comment was made.
    /// </summary>
    public Guid CourseId { get; set; }

    /// <summary>
    /// User id of the commenter.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Content of the comment.
    /// </summary>
    public string content { get; set; }

    /// <summary>
    /// Creation date and time of the comment.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Update date and time of the comment.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Navigation property to the course the comment is made on.
    /// </summary>
    public Course Course { get; set; }

    /// <summary>
    /// Navigation property to the user who made the comment.
    /// </summary>
    public ApplicationUser User { get; set; }
}
