using E_Learning.Domain.Entities;

namespace E_Learning.Repositories.Interfaces
{
    /// <summary>
    /// Comment repository interface for managing comment-related operations.
    /// </summary>
    public interface ICommentRepository
    {
        /// <summary>
        /// Adds a new comment to the database.
        /// </summary>
        /// <param name="comment">The comment entity to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<Comment> AddCommentAsync(Comment comment);

        /// <summary>
        /// Gets all comments for a specific course.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course for which to retrieve comments.</param>
        /// <returns>A list of comment entities.</returns>
        Task<IEnumerable<Comment>> GetByCourseIdAsync(Guid courseId);

        /// <summary>
        /// Gets a comment by its unique identifier.
        /// </summary>
        /// <param name="commentId">The unique identifier of the comment.</param>
        /// <returns>The comment entity if found; otherwise, null.</returns>
        Task<Comment> GetByIdAsync(Guid commentId);

        /// <summary>
        /// Updates an existing comment in the database.
        /// </summary>
        /// <param name="comment">The comment entity with updated data.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateCommentAsync(Comment comment);

        /// <summary>
        /// Deletes a comment from the database by its unique identifier.
        /// </summary>
        /// <param name="commentId">The unique identifier of the comment to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> DeleteCommentAsync(Comment comment);
    }
}