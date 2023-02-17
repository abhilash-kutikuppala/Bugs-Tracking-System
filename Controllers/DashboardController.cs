using BugsTrackingSystem.Services;
using BugsTrackingSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BugsTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _serviceDashboard;
        public DashboardController(IDashboardService serviceDashboard)
        {
            _serviceDashboard = serviceDashboard;
        }

        [Authorize(Policy = "Admin")]
        [HttpGet]
        public async Task<ActionResult<DashboardViewModel>> GetDashboardAsync()
        {
            return Ok(await _serviceDashboard.GetDashboardAsync());
        }
    }
}
