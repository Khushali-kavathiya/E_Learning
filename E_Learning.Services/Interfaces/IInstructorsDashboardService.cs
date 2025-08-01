
using E_Learning.Services.Models;

namespace E_Learning.Services.Interfaces
{
    public interface IInstructorsDashboardService
    {
        /// <summary>
        /// Gets the instructor dashboard data for a specific instructor.
        /// </summary>
        /// <param name="instructorId">The unique identifier of the instructor.</param>
        /// <returns>The instructor dashboard model.</returns>
        Task<InstructorDashboardModel> GetDashboardAsync(string instructorId);
    }
}