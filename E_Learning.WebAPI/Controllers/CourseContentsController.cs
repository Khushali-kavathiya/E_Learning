using E_Learning.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using E_Learning.WebAPI.Contracts;
using E_Learning.Services.Models;
using Microsoft.AspNetCore.JsonPatch;
using E_Learning.Extensions.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace E_Learning.WebAPI.Controllers
{

    /// <summary>
    /// CourseContentsController class handles requests related to course contents.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public class CourseContentsController : ControllerBase
    {
        private readonly ICourseContentsService _courseContentsService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CourseContentsController"/> class.
        /// </summary>
        /// <param name="courseContentsService">The service responsible for handling course content operations.</param>
        /// <param name="mapper">The AutoMapper instance for object-object mapping.</param>
        public CourseContentsController(ICourseContentsService courseContentsService, IMapper mapper)
        {
            _courseContentsService = courseContentsService;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new course content for a specific course.
        /// </summary>
        /// <param name="contract">The contract containing the course content details to create.</param>
        /// <returns>
        /// An ActionResult containing the created course content if successful;
        /// otherwise, returns a BadRequest result with an error message.
        /// </returns>
        [HttpPost]
        [Authorize(Roles = "Instructor, Admin")]
        public async Task<ActionResult> CreateAsync([FromBody] CourseContentContract contract)
        {
            var model = _mapper.Map<CourseContentModel>(contract);
            var created = await _courseContentsService.CreateAsync(model);

            var response = _mapper.Map<CourseContentContract>(created);

            return Ok(response);
        }

        /// <summary>
        /// Retrieves a specific course content by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the course content to retrieve.</param>
        /// <returns>
        /// An ActionResult containing the course content if found;
        /// otherwise, returns a NotFound result with an error message.
        /// </returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetCourseContentByIdAsync(Guid id)
        {
            var courseContent = await _courseContentsService.GetCourseContentByIdAsync(id);
            if (courseContent == null)
                return NotFound("Course content not found.");

            var response = _mapper.Map<CourseContentContract>(courseContent);
            return Ok(response);
        }


        /// <summary>
        /// Retrieves all course contents associated with a specific course.
        /// </summary>
        /// <param name="courseId">The unique identifier of the course for which to retrieve contents.</param>
        /// <returns>
        /// An ActionResult containing a list of course contents if found;
        /// otherwise, returns a NotFound result with an error message.
        /// </returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetCourseContentsByCourseIdAsync(Guid courseId)
        {
            var courseContent = await _courseContentsService.GetCourseContentsByCourseIdAsync(courseId);
            if (courseContent == null)
                return NotFound("Course content not found.");

            var response = _mapper.Map<List<CourseContentContract>>(courseContent);
            return Ok(response);
        }

        /// <summary>
        /// Updates a specific course content by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the course content to update.</param>
        /// <param name="contract">The contract containing the updated course content details.</param>
        /// <returns> The updated course content if successful; otherwise, returns a NotFound result with an error message. </returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Instructor, Admin")]
        public async Task<ActionResult> UpdateCourseContentAsync(Guid id, [FromBody] CourseContentContract contract)
        {
            var model = _mapper.Map<CourseContentModel>(contract);
            var updated = await _courseContentsService.UpdateCourseContentAsync(id, model);
            if (updated == null) return NotFound("Course content not found.");

            return Ok(_mapper.Map<CourseContentContract>(updated));

        }

        /// <summary>
        /// Partially updates a course content using JSON Patch.
        /// </summary>
        /// <param name="id">The unique identifier of the course content to patch.</param>
        /// <param name="patchDoc">A JSON Patch document containing the updates to apply to the course content.</param>
        /// <returns> The updated course content if successful; otherwise, returns a NotFound result with an error message. </returns>
        [HttpPatch("{id}")]
        [Authorize(Roles = "Instructor, Admin")]
        public async Task<ActionResult> PatchCourseContentAsync(Guid id, [FromBody] JsonPatchDocument<CourseContentContract> patchDoc)
        {
            if (patchDoc == null) return BadRequest("Patch document is required.");

            var model = await _courseContentsService.GetCourseContentByIdAsync(id);
            if (model == null) return NotFound("Course content not found.");

            var contract = _mapper.Map<CourseContentContract>(model);

            //Get list of removed operations(for validation)
            var originalOpCount = patchDoc.Operations.Count;
            PatchFilter.RemoveCreateOnlyFields(patchDoc);
            if (patchDoc.Operations.Count == 0)
                return BadRequest("No updatable fields provided or all were restricted by CreateOnly attribute.");
            if (patchDoc.Operations.Count < originalOpCount)
                return BadRequest("Payload contains fields that cannot be update (e.g., CourseId) Please remove them.");

            patchDoc.ApplyTo(contract, ModelState);
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updatedModel = _mapper.Map<CourseContentModel>(contract);
            var result = await _courseContentsService.UpdateCourseContentAsync(id, updatedModel);

            return Ok(_mapper.Map<CourseContentContract>(result));
        }

        /// <summary>
        /// Deletes a specific course content by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the course content to delete.</param>
        /// <returns>
        /// An IActionResult representing the result of the delete operation:
        /// - If the content is successfully deleted, returns an Ok result with a success message.
        /// - If the content is not found, returns a NotFound result with an error message.
        /// </returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Instructor, Admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var deleted = await _courseContentsService.DeleteAsync(id);
            if (!deleted)
                return NotFound("Course content not found.");

            return Ok("Course content deleted successfully.");
        }
    }
}