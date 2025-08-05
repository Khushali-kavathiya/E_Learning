
using E_Learning.Services.Interfaces;
using E_Learning.Services.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Learning.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing course quizzes.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [Authorize]
    public class CourseQuizzesController(ICourseQuizzesService _courseQuizzesService) : ControllerBase
    {
        /// <summary>
        /// Generates a quiz for a course.
        /// </summary>
        /// <param name="model">The course quiz model containing course details.</param>
        /// <returns> An ActionResult containing the created course quiz if successful; otherwise, a 400 Bad Request response.</returns>
        [HttpPost]
        public async Task<IActionResult> GenerateQuiz([FromBody] CourseQuizModel model)
        {
            await _courseQuizzesService.GenerateQuizAsync(model);
            return Ok(new { message = "Quiz generated and saved successfully." });
        }

        /// <summary>
        /// Gets the quiz for a specific course.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course.</param>
        /// <returns> An ActionResult containing the course quiz model if found; otherwise, a 404 Not Found response.</returns>
        [HttpGet]
        public async Task<ActionResult<CourseQuizModel>> GetQuizByCourseId(Guid courseId)
        {
            var quiz = await _courseQuizzesService.GetQuizByCourseIdAsync(courseId);
            return Ok(quiz);
        }
    }
}