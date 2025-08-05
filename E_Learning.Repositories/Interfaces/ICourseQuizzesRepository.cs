using E_Learning.Domain.Entities;
namespace E_Learning.Repositories.Interfaces
{
    public interface ICourseQuizzesRepository
    {
        /// <summary>
        /// Creates a new course quiz in the database.
        /// </summary>
        /// <param name="quiz">The course quiz entity to add.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SaveAsync(CourseQuiz quiz);

        /// <summary>
        /// Gets the course quiz for a specific course.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course.</param>
        /// <returns>The course quiz entity if found; otherwise, null.</returns>
        Task<CourseQuiz?> GetByCourseIdAsync(Guid courseId);
    }
}