
using E_Learning.Services.Models;

namespace E_Learning.Services.Interfaces
{
    /// <summary>
    /// CourseRatingService interface for managing course ratings.
    /// </summary>
    public interface ICourseRatingsService
    {
        /// <summary>
        /// Add or update a course rating.
        /// </summary>
        /// <param name="courseRatingModel">The course rating model to add or update.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<CourseRatingModel> AddOrUpdateRatingAsync(CourseRatingModel courseRatingModel);

        /// <summary>
        /// Get all course ratings associated with a specific course.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course for which to retrieve ratings.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<IEnumerable<CourseRatingModel>> GetRatingsByCourseAsync(Guid courseId);

        /// <summary>
        /// Delete a course rating from the database by its unique identifier.
        /// </summary>
        /// <param name="ratingId">The unique identifier of the course rating to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<bool> DeleteRatingAsync(Guid ratingId);
    }
}