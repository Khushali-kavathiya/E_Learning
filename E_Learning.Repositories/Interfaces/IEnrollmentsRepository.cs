using E_Learning.Domain.Entities;

namespace E_Learning.Repositories.Interfaces
{
    public interface IEnrollmentsRepository
    {
        /// <summary>
        /// Creates a new enrollment in the database.
        /// </summary>
        /// <param name="enrollment">The enrollment entity to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<Enrollment> CreateEnrollmentAsync(Enrollment enrollment);

        /// <summary>
        /// Gets an enrollment by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the enrollment to retrieve.</param>
        /// <returns>The enrollment entity if found; otherwise, null.</returns>
        Task<Enrollment> GetEnrollmentByIdAsync(Guid id);

        /// <summary>
        /// Gets all enrollments for a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A collection of enrollment entities.</returns>
        Task<IEnumerable<Enrollment>> GetEnrollmentsByUserIdAsync(string userId);

        /// <summary>
        /// Updates an existing enrollment in the database.
        /// </summary>
        /// <param name="enrollment">The updated enrollment entity.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateEnrollmentAsync(Enrollment enrollment);

        /// <summary>
        /// Deletes an enrollment from the database by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the enrollment to delete.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task DeleteEnrollmentAsync(Guid id);

        /// <summary>
        /// Get the enrollment for a specific course and user, or null if not found.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course.</param>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The enrollment entity if found; otherwise, null.</returns>
        Task<Enrollment?> GetByCourseAndUserAsync(Guid courseId, string userId);

        /// <summary>
        /// Marks an enrollment as completed.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course.</param>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> MarkAsCompletedAsync(Guid courseId, string userId);

        /// <summary>
        /// Get the enrollment with the specified ID and user, or null if not found.
        /// </summary>
        /// <param name="enrollmentId">The unique identifier of the enrollment.</param>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The enrollment entity if found; otherwise, null.</returns>
        Task<Enrollment?> GetWithCourseAndUserAsync(Guid enrollmentId, string userId);

        /// <summary>
        /// Get all enrollments for a specific course.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course.</param>
        /// <returns>A list of enrollment entities.</returns>
        Task<List<Enrollment>> GetEnrollmentsByCourseIdAsync(Guid couresId);
    }
}