using AutoMapper;
using E_Learning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using E_Learning.WebAPI.Contracts;
using System.Security.Claims;
using E_Learning.Services.Models;
using Microsoft.AspNetCore.JsonPatch;
using E_Learning.Extensions.Helpers;


namespace E_Learning.WebAPI.Controllers
{
    /// <summary>
    /// Controller for managing comments.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("course/{courseId}/[controller]")]
    [Authorize]
    public class CommentsController(ICommentService _commentService, IMapper _mapper) : ControllerBase
    {

        /// <summary>
        /// Add a new comment to a course.
        /// </summary>
        /// <param name="courseId">The course id on which the comment is made.</param>
        /// <param name="contract">The contract containing the comment details.</param>
        /// <returns>The created comment if successful; otherwise, returns a BadRequest result with an error message.</returns>
        [HttpPost]
        public async Task<IActionResult> AddCommentAsync(Guid courseId, [FromBody] CommentContract contract)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized("User not authenticated.");

            var model = _mapper.Map<CommentModel>(contract);
            model.UserId = userId;
            model.CourseId = courseId;

            var result = await _commentService.AddCommentAsync(model);
            return Ok(result);
        }

        /// <summary>
        /// Get all comments made on a specific course.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course for which to retrieve comments.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        [HttpGet]
        public async Task<IActionResult> GetCommentsByCourseIdAsync(Guid courseId)
        {
            var result = await _commentService.GetCommentsByCourseIdAsync(courseId);
            return Ok(result);
        }

        /// <summary>
        /// Update an existing comment.
        /// </summary>
        /// <param name="commentId">The unique identifier of the comment to update.</param>
        /// <param name="patchDocument"> The JSON patch document containing the updated properties.</param>
        /// <returns>Returns the updated comment if successful; otherwise, returns a NotFound result with an error message.</returns>
        [HttpPatch("{commentId}")]
        public async Task<IActionResult> UpdateCommentAsync(Guid commentId, [FromBody] JsonPatchDocument<CommentContract> patchDocument)
        {
            if (patchDocument == null) return BadRequest("patch document is required.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existing = await _commentService.GetByIdAsync(commentId);

            if (existing == null || existing.UserId != userId)
                return Unauthorized("User not authorized to update this comment.");

            var contract = _mapper.Map<CommentContract>(existing);

            //Get list of removed operations(for validation)
            var originalOpCount = patchDocument.Operations.Count;
            PatchFilter.RemoveCreateOnlyFields(patchDocument);
            if (patchDocument.Operations.Count == 0)
                return BadRequest("No updatable fields provided or all were restricted by CreateOnly attribute.");
            if (patchDocument.Operations.Count < originalOpCount)
                return BadRequest("Payload contains fields that cannot be update (e.g., CourseId) Please remove them.");
            patchDocument.ApplyTo(contract, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedModel = _mapper.Map<CommentModel>(contract);
            var updated = await _commentService.UpdateCommentAsync(commentId, updatedModel);

            return Ok(_mapper.Map<CommentContract>(updated));   
            
        }

        /// <summary>
        /// Deletes a specific comment.
        /// </summary>
        /// <param name="commentId">The unique identifier of the comment to delete.</param>
        /// <returns>Returns Ok if successful; otherwise, returns a NotFound result with an error message.</returns>
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteCommentAsync(Guid commentId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized("User not authenticated.");

            var result = await _commentService.DeleteCommentAsync(commentId);
            if (!result) return NotFound("Comment not found.");
            return Ok("Comment deleted successfully.");
        }
    }
}