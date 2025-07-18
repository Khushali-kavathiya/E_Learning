using E_Learning.Domain.Entities;

namespace E_Learning.Repositories.Interfaces
{
    /// <summary>
    /// Repository interface for managing course content-related operations.
    /// </summary>
    public interface ICourseContentsRepository
    {
        /// <summary>
        /// creates a new course content in the database.
        /// </summary>
        /// <param name="content">course content entity to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task AddAsync(CourseContent content);

        /// <summary>
        /// exists a course with the given ID in the database.
        /// </summary>
        /// <param name="courseId">The ID of the course.</param>
        /// <returns>True if the course exists; otherwise, false.</returns>
        Task<bool> CourseExistsAsync(Guid courseId);

        /// <summary>
        /// saves all changes made to the database.
        /// </summary>
        Task SaveChangesAsync();

        /// <summary>
        /// gets a course content by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the course content.</param>
        /// <returns>The course content entity if found; otherwise, null.</returns>
        Task<CourseContent> GetByIdAsync(Guid id);

        /// <summary>
        /// gets all course contents for a specific course.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course.</param>
        /// <returns>A list of course content entities.</returns>
        Task<List<CourseContent>> GetCourseContentsByCourseIdAsync(Guid courseId);

        /// <summary>
        /// deletes a course content from the database by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the course content to delete.</param>
        /// <returns>True if the course content was successfully deleted; otherwise, false.</returns>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Defines the course content entity to be updated.
        /// </summary>
        /// <param name="courseContent">The course content entity with updated data.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateAsync(CourseContent courseContent);
    }
}