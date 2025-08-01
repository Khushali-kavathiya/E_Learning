using E_Learning.Domain.Entities;
using E_Learning.Repositories.Data;
using E_Learning.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace E_Learning.Repositories.Implementations;

/// <inheritdoc />
public class CoursesRepository(E_LearningDbContext _context) : ICoursesRepository
{

    /// <inheritdoc />
    public async Task AddAsync(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<bool> CourseExistsAsync(string title, string instructorId)
        => await _context.Courses.AnyAsync(c =>
            c.Title.ToLower() == title.ToLower() &&
            c.InstructorId == instructorId);

    /// <inheritdoc />
    public async Task<List<Course>> GetCourseAsync()
        => await _context.Courses.ToListAsync();

    /// <inheritdoc />
    public async Task<Course?> GetCourseByIdAsync(Guid courseId)
    {
        return await _context.Courses
            .Include(c => c.Instructor)
            .FirstOrDefaultAsync(c => c.Id == courseId);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Course course)
    {
        _context.Courses.Update(course);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<bool> DeleteCourseAsync(Guid courseId)
    {
        var course = await GetCourseByIdAsync(courseId);
        if (course == null)
            return false;

        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return true;
    }
}
