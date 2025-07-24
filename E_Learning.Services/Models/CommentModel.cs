namespace E_Learning.Services.Models
{
    /// <summary>
    /// CommentModel class for creating a comment.
    /// </summary>
    public class CommentModel
    {
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
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Update date and time of the comment.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}