using Microsoft.AspNetCore.Mvc;
using SIMFranchise.DTOs.Reports;
using SIMFranchise.DTOs.Sales;
using SIMFranchise.Interfaces;
using SIMFranchise.Wrappers;

namespace SIMFranchise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        // 1. Franchise Grand Summary Report 
        // GET: api/Report/franchise-grand-summary?franchiseId=1&filter=TODAY
        [HttpGet("franchise-grand-summary")]
        public async Task<IActionResult> GetGrandSummary([FromQuery] int franchiseId, [FromQuery] string filter)
        {
            if (string.IsNullOrEmpty(filter))
            {
                return BadRequest(ApiResponse<string>.FailureResponse("Filter type (Today, Weekly, etc.) is required."));
            }

            try
            {
                var report = await _reportService.GetFranchiseGrandReportAsync(franchiseId, filter);

                if (report == null)
                {
                    return NotFound(ApiResponse<string>.FailureResponse("No data found for the selected period."));
                }

                return Ok(ApiResponse<FranchiseGrandReportDto>.SuccessResponse(report, "Franchise grand report retrieved successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailureResponse($"Internal server error: {ex.Message}"));
            }
        }

        // Endpoint for Page 2: Member Breakdown
        [HttpGet("member-sale-report")]
        public async Task<IActionResult> GetMemberSaleReport([FromQuery] int franchiseId, [FromQuery] string filter)
        {
            try
            {
                // 1. Service se data mangwaya
                var report = await _reportService.GetMemberSaleReportAsync(franchiseId, filter);

                // 2. Response mein DTO ki list return ki (GetMemberSaleReportAsync ki jagah MemberSaleReportDto aayega)
                return Ok(ApiResponse<List<MemberSaleReportDto>>.SuccessResponse(report, "Member sale report retrieved."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailureResponse($"Error: {ex.Message}"));
            }
        }
    }
}