using Microsoft.AspNetCore.Mvc; 
using SIMFranchise.DTOs.Closing; 
using SIMFranchise.Interfaces.Sales;
using SIMFranchise.Models;
using SIMFranchise.Wrappers;

namespace SIMFranchise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClosingController : ControllerBase
    {
        private readonly IDailyClosingService _closingService;

        public ClosingController(IDailyClosingService closingService)
        {
            _closingService = closingService;
        }

        // 1. Raat ki Closing Record karna  
        [HttpPost("perform-closing")]
        public async Task<IActionResult> PerformClosing([FromBody] DailyClosingCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(ApiResponse<string>.FailureResponse("Closing data is required."));
            }

            try
            {
                var success = await _closingService.PerformDailyClosingAsync(dto);

                if (!success)
                {
                    return BadRequest(ApiResponse<string>.FailureResponse("Could not perform closing. Maybe it's already closed for today?"));
                }

                return Ok(ApiResponse<string>.SuccessResponse(  "Day closed successfully. Inventory and Cash reconciled."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailureResponse($"Error: {ex.Message}"));
            }
        }
         
        [HttpGet("closing-history/{franchiseId}")]
        public async Task<IActionResult> GetClosingHistory(int franchiseId)
        {
            try
            {
                var history = await _closingService.GetClosingHistoryAsync(franchiseId);

                if (history == null || !history.Any())
                {
                    return NotFound(ApiResponse<string>.FailureResponse("No closing history found for this franchise."));
                }

                return Ok(ApiResponse<IEnumerable<DailyClosing>>.SuccessResponse(history, "Closing history retrieved successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailureResponse($"Error: {ex.Message}"));
            }
        }
    }
}