using E_Learning.Domain.Entities;
using E_Learning.Services.Models;

namespace E_Learning.Services.Interfaces;

/// <summary>
/// Service interface for handling course-related operations such as creation,
/// retrieval, update, and deletion of course data.
/// </summary>
public interface ICoursesService
{
    /// <summary>
    /// Creates a new course based on the provided course model.
    /// </summary>
    /// <param name="model">The course data from the service layer.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task CreateCourseAsync(CourseModel model);

    /// <summary>
    /// Retrieves a list of all courses available in the system.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation, containing a list of <see cref="CourseModel"/>.
    /// </returns>
    Task<List<CourseModel>> GetCoursesAsync();

    /// <summary>
    /// Retrieves a specific course by its unique identifier.
    /// </summary>
    /// <param name="courseId">The ID of the course to retrieve.</param>
    /// <returns>
    /// A task representing the asynchronous operation, containing the <see cref="CourseModel"/>
    /// if found; otherwise, null.
    /// </returns>
    Task<CourseModel> GetCourseByIdAsync(Guid courseId);

    /// <summary>
    /// Updates an existing course with the specified data.
    /// </summary>
    /// <param name="courseId">The ID of the course to update.</param>
    /// <param name="updatedModel">The updated course data.</param>
    /// <returns>
    /// A task representing the asynchronous operation, returning true if the update succeeded; otherwise, false.
    /// </returns>
    Task<bool> UpdateCourseAsync(Guid courseId, CourseModel updatedModel);

    /// <summary>
    /// Deletes a course identified by its unique ID.
    /// </summary>
    /// <param name="courseId">The ID of the course to delete.</param>
    /// <returns>
    /// A task representing the asynchronous operation, returning true if the deletion succeeded; otherwise, false.
    /// </returns>
    Task<bool> DeleteCourseAsync(Guid courseId);
}