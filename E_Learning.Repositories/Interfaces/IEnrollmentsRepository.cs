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
    }
}