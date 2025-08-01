
using E_Learning.Services.Models;

namespace E_Learning.Services.Interfaces
{
    public interface IStudentDashboardService
    {
        /// <summary>
        /// Gets the student dashboard data for a specific student.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The student dashboard model.</returns>
        Task<StudentDashboardModel> GetDashboardAsync(string userId);
    }
}