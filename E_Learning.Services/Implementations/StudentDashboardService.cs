using AutoMapper;
using E_Learning.Repositories.Interface;
using E_Learning.Repositories.Interfaces;
using E_Learning.Services.Interfaces;
using E_Learning.Services.Models;
namespace E_Learning.Services.Implementations
{
    /// <summary>
    /// StudentDashboardService class implements IStudentDashboardService interface for student dashboard-related operations.
    /// </summary>
    public class StudentDashboardService(IEnrollmentsRepository _enrollmentsRepository, ICoursesRepository _coursesRepository, ICourseRatingsRepository _courseRatingRepository, IMapper _mapper) : IStudentDashboardService
    {
        /// <inheritdoc>
        public async Task<StudentDashboardModel> GetDashboardAsync(string userId)
        {
            var enrollments = await _enrollmentsRepository.GetEnrollmentsByUserIdAsync(userId);
            var dashboard = new StudentDashboardModel
            {
                TotalCoursesEnrolled = enrollments.Count(),
                TotalCoursesCompleted = enrollments.Count(e => e.IsCompleted),
                Courses = new()
            };

            foreach (var enrollment in enrollments)
            {
                var course = await _coursesRepository.GetCourseByIdAsync(enrollment.CourseId);
                var rating = await _courseRatingRepository.GetRatingByUserAndCourseAsync(userId, enrollment.CourseId);

                var courseModel = _mapper.Map<StudentDashboardCourseModel>(course);
                courseModel.IsCompleted = enrollment.IsCompleted;
                courseModel.RatingGiven = rating?.Rating;
                dashboard.Courses.Add(courseModel);
            }
            return dashboard;
        }
    }
}