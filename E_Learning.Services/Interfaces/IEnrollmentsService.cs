using E_Learning.Domain.Entities;
using E_Learning.Services.Models;

namespace E_Learning.Services.Interfaces
{
    public interface IEnrollmentsService
    {
        /// <summary>
        /// Creates a new enrollment for a user in a course.
        /// </summary>
        /// <param name="enrollmentModel">The enrollment model containing user and course details.</param>
        /// <returns>The created enrollment model.</returns>
        Task<EnrollmentModel> CreateEnrollmentAsync(EnrollmentModel enrollmentModel);

        /// <summary>
        /// Gets specific enrollment details by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the enrollment.</param>
        /// <returns>The enrollment model if found; otherwise, null.</returns>
        Task<EnrollmentModel> GetEnrollmentByIdAsync(Guid id);

        /// <summary>
        /// Gets all enrollments for a specific user.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A collection of enrollment models.</returns>
        Task<IEnumerable<EnrollmentModel>> GetByUserIdAsync(string userId);

        /// <summary>
        /// Updates an existing enrollment.
        /// </summary>
        /// <param name="enrollmentModel">The updated enrollment model.</param>
        /// <returns>The updated enrollment model.</returns>
        Task UpdateEnrollmentAsync(Guid id, EnrollmentModel enrollmentModel);

        /// <summary>
        /// Deletes an enrollment by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the enrollment.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<bool> DeleteEnrollmentAsync(Guid id);

        /// <summary>
        /// Marks a course as completed for a specific user.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course.</param>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task<bool> MarkCourseAsCompletedAsync(Guid courseId, string userId);

        /// <summary>
        /// Gets the enrollment for a specific course and user, or null if not found.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course.</param>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The enrollment model if found; otherwise, null.</returns>
        Task<Enrollment?> GetEnrollmentWithUserAndCourseAsync(Guid enrollmentId, string userId);
    }
}