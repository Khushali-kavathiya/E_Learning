using E_Learning.Services.Models;
namespace E_Learning.Services.Interfaces
{
    /// <summary>
    /// Service interface for handling comment-related operations such as adding,
    /// update, and deleting comments.
    /// </summary>
    public interface ICommentService
    {
        /// <summary>
        /// Add a new comment to the course.
        /// </summary>
        /// <param name="commentModel">The comment data from the service layer.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<CommentModel> AddCommentAsync(CommentModel commentModel);

        /// <summary>
        /// Get all comments made on a specific course.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course for which to retrieve comments.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<IEnumerable<CommentModel>> GetCommentsByCourseIdAsync(Guid courseId);

        /// <summary>
        /// Get a specific comment by its unique identifier.
        /// </summary>
        /// <param name="commentId">The unique identifier of the comment to retrieve.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<CommentModel> GetByIdAsync(Guid commentId);

        /// <summary>
        /// Update a comment with the specified data.
        /// </summary>
        /// <param name="id">The unique identifier of the comment to update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<CommentModel> UpdateCommentAsync(Guid id, CommentModel commentModel);

        /// <summary>
        /// Delete a comment with the specified unique identifier.
        /// </summary>
        /// <param name="commentId">The unique identifier of the comment to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<bool> DeleteCommentAsync(Guid commentId);
    }
}