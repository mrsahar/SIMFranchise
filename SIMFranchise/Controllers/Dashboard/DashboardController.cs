using Microsoft.AspNetCore.Mvc; 
using SIMFranchise.DTOs.Dashboard;
using SIMFranchise.Interfaces;
using SIMFranchise.Interfaces.Dashboard;
using SIMFranchise.Wrappers;

namespace SIMFranchise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // GET: api/Dashboard/main-summary/1
        [HttpGet("main-summary/{franchiseId}")]
        public async Task<IActionResult> GetMainSummary(int franchiseId)
        {
            try
            {
                var dashboardData = await _dashboardService.GetFranchiseDashboardAsync(franchiseId);

                return Ok(ApiResponse<FranchiseDashboardDto>.SuccessResponse(
                    dashboardData,
                    "Main dashboard data retrieved successfully."
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailureResponse(ex.Message));
            }
        }
    }
}