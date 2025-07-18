using E_Learning.Services.Models;

namespace E_Learning.Services.Interfaces
{
    /// <summary>
    /// interface for handling course content-related operations such as creation,
    /// getting, updating, and deleting course content data.
    /// </summary>
    public interface ICourseContentsService
    {
        /// <summary>
        /// creates a new course content based on the provided course content model.
        /// </summary>
        /// <param name="model">The course content data from the service layer.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<CourseContentModel> CreateAsync(CourseContentModel model);

        /// <summary>
        /// gets a specific course content by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the course content to retrieve.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<CourseContentModel> GetCourseContentByIdAsync(Guid id);

        /// <summary>
        /// gets all course contents associated with a specific course.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course for which to retrieve contents.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<List<CourseContentModel>> GetCourseContentsByCourseIdAsync(Guid courseId);

        /// <summary>
        /// deletes a specific course content by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the course content to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Updates a specific course content based on the provided course content model.
        /// </summary>
        /// <param name="id">The unique identifier of the course content to update.</param>
        /// <param name="model">The updated course content data from the service layer.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<CourseContentModel> UpdateCourseContentAsync(Guid id, CourseContentModel model);
    }
}