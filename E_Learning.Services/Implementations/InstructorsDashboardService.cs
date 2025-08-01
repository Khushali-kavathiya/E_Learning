
using AutoMapper;
using E_Learning.Repositories.Interface;
using E_Learning.Repositories.Interfaces;
using E_Learning.Services.Interfaces;
using E_Learning.Services.Models;

namespace E_Learning.Services.Implementations
{
    /// <summary>
    /// InstructorsDashboardService class implements IInstructorsDashboardService interface for instructor dashboard-related operations.
    /// </summary>

    public class InstructorsDashboardService(ICoursesRepository _coursesRepository, IEnrollmentsRepository _enrollmentsRepository, ICourseRatingsRepository _ratingsRepository, IMapper _mapper) : IInstructorsDashboardService
    {
        /// <inheritdoc>
        public async Task<InstructorDashboardModel> GetDashboardAsync(string instructorId)
        {
            var allCourses = await _coursesRepository.GetCourseAsync();
            var instructorCourses = allCourses.Where(c => c.InstructorId == instructorId).ToList();

            var dashboard = new InstructorDashboardModel
            {
                TotalCourses = instructorCourses.Count,
                Courses = new List<InstructorDashboardCourseModel>()
            };

            foreach (var course in instructorCourses)
            {
                var enrollments = await _enrollmentsRepository.GetEnrollmentsByCourseIdAsync(course.Id);
                var ratings = await _ratingsRepository.GetAllByCourseAsync(course.Id);

                int completionCount = enrollments.Count(e => e.IsCompleted);
                int enrollmentCount = enrollments.Count;
                double avgRating = ratings.Any() ? ratings.Average(r => r.Rating) : 0;
                decimal earnings = course.IsFree ? 0 : course.Price * enrollmentCount;

                dashboard.TotalEnrollments += enrollmentCount;
                dashboard.TotalEarnings += earnings;

                var courseModel = _mapper.Map<InstructorDashboardCourseModel>(course);
                courseModel.EnrollmentCount = enrollmentCount;
                courseModel.CompletionCount = completionCount;
                courseModel.AverageRating = avgRating;
                courseModel.Earnings = earnings;

                dashboard.Courses.Add(courseModel);
            }
            return dashboard;
        }
    } 
}