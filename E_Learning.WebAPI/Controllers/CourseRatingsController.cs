
using AutoMapper;
using E_Learning.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using E_Learning.WebAPI.Contracts;
using System.Security.Claims;
using E_Learning.Services.Models;
using Microsoft.AspNetCore.Authorization;

namespace E_Learning.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing course ratings.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [Authorize]
    public class CourseRatingsController(ICourseRatingsService _courseRatingsService, IMapper _mapper) : ControllerBase
    {

        /// <summary>
        /// Add or update a course rating.
        /// </summary>
        /// <param name="courseId">The course id for which the rating was made.</param>
        /// <param name="contract">The course rating data from the service layer.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [HttpPost]
        public async Task<IActionResult> AddOrUpdateRatingAsync(Guid courseId, [FromBody] CourseRatingContract contract)
        {
            if (contract.Rating < 1 || contract.Rating > 5)
                return BadRequest("Rating should be between 1 and 5.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("User not authenticated.");

            var model = _mapper.Map<CourseRatingModel>(contract);
            model.CourseId = courseId;
            model.UserId = userId;

            var result = await _courseRatingsService.AddOrUpdateRatingAsync(model);
            return Ok(_mapper.Map<CourseRatingContract>(result));
        }

        /// <summary>
        /// Get all course ratings associated with a specific course.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course for which to retrieve ratings.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [HttpGet]
        public async Task<IActionResult> GetRatingsByCourseAsync(Guid courseId)
        {
            var rating = await _courseRatingsService.GetRatingsByCourseAsync(courseId);
            var avarageRating = rating.Any() ? rating.Average(r => r.Rating) : 0;
            var totalLikes = rating.Count(r => r.Rating >= 4);

            return Ok(new
            {
                AverageRating = avarageRating,
                TotalLikes = totalLikes,
                Ratings = _mapper.Map<IEnumerable<CourseRatingContract>>(rating)
            });
        }

        /// <summary>
        /// Delete a specific course rating by course and user.
        /// </summary>
        /// <param name="ratingId">The unique identifier of the rating to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [HttpDelete("{ratingId}")]
        public async Task<IActionResult> DeleteRatingAsync(Guid ratingId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized("User not authenticated.");

            var deleted = await _courseRatingsService.DeleteRatingAsync(ratingId);
            if (!deleted) return NotFound("Rating not found.");

            return Ok("Rating deleted successfully.");
        }
    }
}