using E_Learning.Extensions.Attributes;
using E_Learning.Extensions.Mappings;
using E_Learning.Services.Models;

namespace E_Learning.WebAPI.Contracts
{
    /// <summary>
    /// CommentContract class for creating a comment.
    /// </summary>
    public class CommentContract : IMapFrom<CommentModel>
    {
        /// <summary>
        /// Course id on which the comment was made.
        /// </summary>
        [CreateOnly]
        public Guid CourseId { get; set; }

        /// <summary>
        /// User id of the commenter.
        /// </summary>
        [CreateOnly]
        public string? UserId { get; set; }

        /// <summary>
        /// Content of the comment.
        /// </summary>
        public string? content { get; set; }

        /// <summary>
        /// Creation date and time of the comment.
        /// </summary>
        [CreateOnly]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Update date and time of the comment.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}