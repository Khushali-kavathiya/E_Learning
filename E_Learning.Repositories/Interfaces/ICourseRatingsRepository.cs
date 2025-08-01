
using E_Learning.Domain.Entities;

namespace E_Learning.Repositories.Interfaces
{
    /// <summary>
    /// Course rating repository interface for managing course rating-related operations.
    /// </summary>
    public interface ICourseRatingsRepository
    {
        /// <summary>
        /// Adds or updates a course rating in the database.
        /// </summary>
        /// <param name="rating">The course rating entity to add or update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<CourseRating> AddOrUpdateRatingAsync(CourseRating rating);

        /// <summary>
        /// Gets all course ratings for a specific course.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course.</param>
        /// <returns>A collection of course rating entities.</returns>
        Task<IEnumerable<CourseRating>> GetAllByCourseAsync(Guid courseId);

        /// <summary>
        /// Gets a course rating by its unique identifier.
        /// </summary>
        /// <param name="ratingId">The unique identifier of the course rating.</param>
        /// <returns>The course rating entity if found; otherwise, null.</returns>
        Task<CourseRating> GetByIdAsync(Guid ratingId);

        /// <summary>
        /// Deletes a course rating from the database by its unique identifier.
        /// </summary>
        /// <param name="ratingId">The unique identifier of the course rating to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> DeleteAsync(CourseRating rating);

        /// <summary>
        /// Gets the course rating for a specific user and course.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="courseId">The unique identifier of the course.</param>
        /// <returns>The course rating entity if found; otherwise, null.</returns>
        Task<CourseRating?> GetRatingByUserAndCourseAsync(string userId, Guid courseId);
    }
}