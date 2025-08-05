
using E_Learning.Services.Models;

namespace E_Learning.Services.Interfaces
{
    /// <summary>
    /// CourseQuizService interface for managing course quizzes.
    /// </summary>
    public interface ICourseQuizzesService
    {
        /// <summary>
        /// Generates a quiz for a course.
        /// </summary>
        /// <param name="model">The course quiz model containing course details.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task GenerateQuizAsync(CourseQuizModel model);

        /// <summary>
        /// Gets the quiz for a specific course.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course.</param>
        /// <returns>The course quiz model if found; otherwise, null.</returns>
        Task<CourseQuizModel> GetQuizByCourseIdAsync(Guid courseId);
    }
}