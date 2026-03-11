using Microsoft.AspNetCore.Mvc; 
using SIMFranchise.DTOs.Stock;
using SIMFranchise.Interfaces;
using SIMFranchise.Wrappers;

namespace SIMFranchise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributionController : ControllerBase
    {
        private readonly IDistributionService _distService;

        public DistributionController(IDistributionService distService)
        {
            _distService = distService;
        }

        // 1. Issue Stock to Member (Dukan se Member ko maal dena)
        [HttpPost("issue-stock")]
        public async Task<IActionResult> IssueStock([FromBody] StockDistributionDto dto)
        {
            var success = await _distService.IssueStockToMemberAsync(dto);
            if (!success)
            {
                return BadRequest(ApiResponse<string>.FailureResponse("Insufficient franchise stock or invalid data."));
            }
            return Ok(ApiResponse<string>.SuccessResponse(null, "Stock issued to team member successfully."));
        }

        // 2. Return Stock from Member (Member se bacha hua maal wapas lena)
        [HttpPost("return-stock")]
        public async Task<IActionResult> ReturnStock([FromBody] StockDistributionDto dto)
        {
            var success = await _distService.ReturnStockToFranchiseAsync(dto);
            if (!success)
            {
                return BadRequest(ApiResponse<string>.FailureResponse("Member does not have enough stock to return."));
            }
            return Ok(ApiResponse<string>.SuccessResponse(null, "Stock returned to franchise successfully."));
        }

        // 3. Get Member's Live Stock (Ali ke pas is waqt kya kya hai?)
        [HttpGet("member-live-stock/{memberId}")]
        public async Task<IActionResult> GetMemberStock(int memberId)
        {
            var stock = await _distService.GetMemberCurrentStockAsync(memberId);
            return Ok(ApiResponse<object>.SuccessResponse(stock, "Member current stock retrieved."));
        }

        // 4. Get Distribution History (Audit Trail)
        [HttpGet("logs/{franchiseId}")]
        public async Task<IActionResult> GetLogs(int franchiseId)
        {
            var logs = await _distService.GetDistributionLogsAsync(franchiseId);
            return Ok(ApiResponse<object>.SuccessResponse(logs, "Distribution history retrieved."));
        }
    }
}