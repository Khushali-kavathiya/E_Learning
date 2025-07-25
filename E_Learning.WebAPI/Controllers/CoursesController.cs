using System.Security.Claims;
using AutoMapper;
using E_Learning.Services.Interfaces;
using E_Learning.Services.Models;
using E_Learning.WebAPI.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using E_Learning.Extensions.Helpers;

namespace E_Learning.WebAPI.Controllers;

/// <summary>
/// Controller for managing Courses.
/// </summary>

[ApiController]
[ApiVersion("1.0")]
[Route("[controller]")]
[Authorize(Roles = "Instructor, Admin")]

public class CoursesController(ICoursesService _coursesService, IMapper _mapper) : ControllerBase
{
    /// <summary>
    /// Create a new Coursess by the logged-in instructor.
    /// </summary>
    /// <param name="request">The CourseContract object containing the course details.</param>
    /// <returns>Returns an IActionResult representing the result of the course creation process.</returns>

    [HttpPost]
    public async Task<IActionResult> CreateCourses([FromBody] CourseContract request)
    {
        var model = _mapper.Map<CourseModel>(request);
        model.InstructorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        await _coursesService.CreateCourseAsync(model);
        return Ok("Course created successfully.");
    }

    /// <summary>
    /// Retrieves a specific course by its unique identifier.
    /// </summary>
    /// <param name="courserId">The unique identifier (GUID) of the course to retrieve.</param>
    /// <returns>
    /// An ActionResult containing the CourseContract for the specified course.
    /// Returns an OK (200) status with the course data if found, or a NotFound (404) status if the course doesn't exist.
    /// </returns>
    [HttpGet("{courserId}")]
    public async Task<ActionResult> GetCoursesById(Guid courserId)
    {
        var course = await _coursesService.GetCourseByIdAsync(courserId);
        if (course == null)
            return NotFound("Course not found.");
        var response = _mapper.Map<CourseContract>(course);
        return Ok(response);
    }

    /// <summary>
    /// Retrieves all courses from the system.
    /// </summary>
    /// <returns>
    /// An ActionResult containing a list of CourseContract objects.
    /// Returns an OK (200) status with the list of courses if successful.
    /// </returns>
    [HttpGet]
    public async Task<ActionResult> GetAllCourses()
    {
        var CoursesService = await _coursesService.GetCoursesAsync();
        var response = _mapper.Map<List<CourseContract>>(CoursesService);
        return Ok(response);
    }

    /// <summary>
    /// Partially updates a course using JSON Patch.
    /// </summary>
    /// <param name="courseId">The unique identifier of the course to update.</param>
    /// <param name="pathDoc">A JSON Patch document containing the updates to apply to the course.</param>
    /// <returns>
    /// An IActionResult representing the result of the operation:
    /// - 200 OK if the course was successfully updated.
    /// - 400 Bad Request if the patch document is null, no valid updatable fields were provided, or some fields cannot be updated.
    /// - 404 Not Found if the course with the specified ID does not exist.
    /// </returns>
    [HttpPatch("{courseId}")]
    public async Task<IActionResult> PatchCourses(Guid courseId, [FromBody] JsonPatchDocument<CourseContract> patchDoc)
    {
        if (patchDoc == null)
            return BadRequest();

        var existing = await _coursesService.GetCourseByIdAsync(courseId);
        if (existing == null)
            return NotFound();

        var contract = _mapper.Map<CourseContract>(existing);
        var originalOpCount = patchDoc.Operations.Count;

        // Remove operations that attempt to modify [CreateOnly] fields
        PatchFilter.RemoveCreateOnlyFields(patchDoc);
        if (patchDoc.Operations.Count == 0)
            return BadRequest("No valid updatable fields were provided. Fields like InstructorId or CreatedAt are restricted and cannot be modified.");
        if (patchDoc.Operations.Count < originalOpCount)
            return BadRequest("Some fields in your request (e.g., InstructorId, CreatedAt) cannot be updated and were ignored. Please remove them from your patch payload.");

        patchDoc.ApplyTo(contract, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updatedModel = _mapper.Map<CourseModel>(contract);
        var updated = await _coursesService.UpdateCourseAsync(courseId, updatedModel);
        return updated ? Ok("Course updated successfully") : BadRequest("Update failed.");
    }


    /// <summary>
    /// Deletes a specific course from the system.
    /// </summary>
    /// <param name="courseId">The unique identifier (GUID) of the course to be deleted.</param>
    /// <returns>
    /// An IActionResult representing the result of the delete operation:
    /// - 200 OK with a success message if the course was successfully deleted.
    /// - 404 Not Found with an error message if the course with the specified ID does not exist.
    /// </returns>
    [HttpDelete("{courseId}")]
    public async Task<IActionResult> DeleteCourse (Guid courseId)
    {
        var deleted = await _coursesService.DeleteCourseAsync(courseId);

        if (!deleted)
            return NotFound("Course not found.");

        return Ok("Course deleted successfully.");
    }
}