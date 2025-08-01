using System.Security.Claims;
using E_Learning.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace E_Learning.WebAPI.Controllers
{
    /// <summary>
    /// InstructorsDashboardController class handles requests related to instructor dashboards.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [Authorize(Roles = "Instructor, Admin")]
    public class InstructorsDashboardController(IInstructorsDashboardService _dashboardService) : ControllerBase
    {
        /// <summary>
        /// Gets the instructor dashboard data for the currently logged-in instructor.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult> GetDashboard()
        {
            var instructorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dashboard = await _dashboardService.GetDashboardAsync(instructorId);
            return Ok(dashboard);

        }
    }
}