using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using E_Learning.Services.Interfaces;
using E_Learning.Services.Models;
using E_Learning.WebAPI.Contracts;
using Microsoft.AspNetCore.JsonPatch;
using E_Learning.Extensions.Helpers;

namespace E_Learning.WebAPI.Controllers;

/// <summary>
/// Controller for managing Enrollments.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("[controller]")]
[Authorize]
public class EnrollmentsController(IEnrollmentsService _service, IMapper _mapper) : ControllerBase
{

    /// <summary>
    /// Enrolls a user in a course.
    /// </summary>
    /// <param name="contract">The contract containing the user and course details for enrollment.</param>
    /// <returns>The created enrollment if successful; otherwise, returns a BadRequest result with an error message.</returns>
    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] EnrollmentContract contract)
    {
        var model = _mapper.Map<EnrollmentModel>(contract);
        var result = await _service.CreateEnrollmentAsync(model);
        return Ok(_mapper.Map<EnrollmentContract>(result));
    }

    /// <summary>
    /// Gets an enrollment by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the enrollment to retrieve.</param>
    /// <returns>The enrollment if found; otherwise, returns a NotFound result with an error message.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult> GetByIdAsync(Guid id)
    {
        var result = await _service.GetEnrollmentByIdAsync(id);
        if (result == null) return NotFound("Enrollment not found.");
        return Ok(_mapper.Map<EnrollmentContract>(result));
    }

    /// <summary>
    /// Gets all enrollments for a specific user.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <returns>A collection of enrollment models.</returns>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult> GetByUserIdAsync(string userId)
    {
        var result = await _service.GetByUserIdAsync(userId);
        if (result == null) return NotFound("Enrollments not found.");
        return Ok(_mapper.Map<IEnumerable<EnrollmentContract>>(result));
    }

    /// <summary>
    /// Updates an existing enrollment.
    /// </summary>
    /// <param name="id">The unique identifier of the enrollment to update.</param>
    /// <param name="contract">The updated enrollment contract.</param>
    /// <returns>Returns the updated enrollment if successful; otherwise, returns a NotFound result with an error message.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] EnrollmentContract contract)
    {
        var model = _mapper.Map<EnrollmentModel>(contract);
        await _service.UpdateEnrollmentAsync(id, model);
        return Ok("Enrollment updated successfully.");
    }

    /// <summary>
    /// Partially updates an existing enrollment.
    /// </summary>
    /// <param name="id">The unique identifier of the enrollment to update.</param>
    /// <param name="patchDoc">The JSON patch document containing the updated properties.</param>
    /// <returns>Returns the updated enrollment if successful; otherwise, returns a NotFound result with an error message.</returns>
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchAsync(Guid id, [FromBody] JsonPatchDocument<EnrollmentContract> patchDoc)
    {
        if (patchDoc == null) return BadRequest();
        var model = await _service.GetEnrollmentByIdAsync(id);
        var contract = _mapper.Map<EnrollmentContract>(model);

        var originalOpCount = patchDoc.Operations.Count;
        // Remove operations that attempt to modify [CreateOnly] fields
        PatchFilter.RemoveCreateOnlyFields(patchDoc);
        if (patchDoc.Operations.Count == 0)
            return BadRequest("No valid updatable fields were provided. Fields like UserId or CourseId or EnrolledAt are restricted and cannot be modified.");
        if (patchDoc.Operations.Count < originalOpCount)
            return BadRequest("Some fields in your request (e.g., UserId, CourseId, EnrolledAt) cannot be updated and were ignored. Please remove them from your patch payload.");

        patchDoc.ApplyTo(contract, ModelState);
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updatedModel = _mapper.Map<EnrollmentModel>(contract);
        await _service.UpdateEnrollmentAsync(id, updatedModel);

        return Ok("Enrollment updated successfully.");
    }

    /// <summary>
    /// Deletes an enrollment by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the enrollment to delete.</param>
    /// <returns>returns a NotFound result if the enrollment is not found; otherwise, returns an Ok result with a success message.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var deleted = await _service.DeleteEnrollmentAsync(id);
        if (!deleted) return NotFound("Enrollment not found.");
        return Ok("Enrollment deleted successfully.");
    }
}
