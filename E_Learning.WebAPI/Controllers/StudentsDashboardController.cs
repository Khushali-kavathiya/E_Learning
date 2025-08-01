using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using E_Learning.Services.Interfaces;
using System.Security.Claims;

namespace E_Learning.WebAPI.Controllers
{
    /// <summary>
    /// StudentsDashboardController class handles requests related to student dashboards.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    [Authorize]
    public class StudentsDashboardController(IStudentDashboardService _studentDashboardService) : ControllerBase
    {
        /// <summary>
        /// Gets the student dashboard data for the currently logged-in student.
        /// </summary>
        /// <returns>The student dashboard model.</returns>
        [HttpGet]
        public async Task<ActionResult> GetStudentDashboard()
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var dashboard = await _studentDashboardService.GetDashboardAsync(studentId);
            return Ok(dashboard);
        }
    }
}