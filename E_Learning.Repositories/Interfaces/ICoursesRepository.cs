using E_Learning.Domain.Entities;

namespace E_Learning.Repositories.Interface;

/// <summary>
/// Repository interface for managing course-related operations.
/// </summary>
public interface ICoursesRepository
{
    /// <summary>
    /// Adds a new course to the database.
    /// </summary>
    /// <param name="course">The course entity to add.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task AddAsync(Course course);

    /// <summary>
    /// Checks if a course with the given title already exists for the specified instructor.
    /// </summary>
    /// <param name="title">The title of the course.</param>
    /// <param name="instructorId">The ID of the instructor.</param>
    /// <returns>True if the course exists; otherwise, false.</returns>
    Task<bool> CourseExistsAsync(string title, string instructorId);

    /// <summary>
    /// Retrieves all courses from the database.
    /// </summary>
    /// <returns>A list of all course entities.</returns>
    Task<List<Course>> GetCourseAsync();

    /// <summary>
    /// Retrieves a course by its unique identifier.
    /// </summary>
    /// <param name="courseId">The unique identifier of the course.</param>
    /// <returns>The course entity if found; otherwise, null.</returns>
    Task<Course> GetCourseByIdAsync(Guid courseId);

    /// <summary>
    /// Updates an existing course in the database.
    /// </summary>
    /// <param name="course">The course entity with updated data.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(Course course);

    /// <summary>
    /// Deletes a course from the database by its ID.
    /// </summary>
    /// <param name="courseId">The unique identifier of the course to delete.</param>
    /// <returns>True if the course was successfully deleted; otherwise, false.</returns>
    Task<bool> DeleteCourseAsync(Guid courseId);
}
