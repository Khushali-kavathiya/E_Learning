using AutoMapper;
using E_Learning.Domain.Entities;
using E_Learning.Repositories.Interface;
using E_Learning.Services.Interfaces;
using E_Learning.Services.Models;

namespace E_Learning.Services.Implementations;

public class CoursesService(ICoursesRepository _coursesRepository, IMapper _mapper) : ICoursesService
{
    /// <inheritdoc>
    public async Task CreateCourseAsync(CourseModel model)
    {
        bool exists = await _coursesRepository.CourseExistsAsync(model.Title, model.InstructorId);
        if (exists)
            throw new InvalidOperationException("A course with the same title already exists for this instructor.");
        var course = _mapper.Map<Course>(model);
        await _coursesRepository.AddAsync(course);
    }

    /// <inheritdoc>
    public async Task<List<CourseModel>> GetCoursesAsync()
    {
        var courses = await _coursesRepository.GetCourseAsync();
        if (courses == null)
            return null;

        var result = _mapper.Map<List<CourseModel>>(courses);
        return result;
    }

    /// <inheritdoc>
    public async Task<CourseModel> GetCourseByIdAsync(Guid courseId)
    {
        var course = await _coursesRepository.GetCourseByIdAsync(courseId);
        if (course == null)
            return null;

        var result = _mapper.Map<CourseModel>(course);
        return result;
    }

    /// <inheritdoc>
    public async Task<bool> UpdateCourseAsync(Guid courseId, CourseModel updatedModel)
    {
        var course = await _coursesRepository.GetCourseByIdAsync(courseId);
        if (course == null)
            return false;
        // 🔐 Preserve critical fields before overwriting with mapper
        var instructorId = course.InstructorId;

        // Map new fields
        _mapper.Map(updatedModel, course);

        // 🔒 Restore preserved values
        course.InstructorId = instructorId;


        await _coursesRepository.UpdateAsync(course);
        return true;
    }

    /// <inheritdoc>
    public async Task<bool> DeleteCourseAsync(Guid courseId)
    {
        var course = await _coursesRepository.DeleteCourseAsync(courseId);
        if (course == null)
            return false;
        return true;
    }
}