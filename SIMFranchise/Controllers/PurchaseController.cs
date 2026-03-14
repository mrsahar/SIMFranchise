using Microsoft.AspNetCore.Mvc;

using SIMFranchise.DTOs.Purchase;
using SIMFranchise.Interfaces.Purchase;
using SIMFranchise.Wrappers;

namespace SIMFranchise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPurchase(PurchaseCreateDto dto)
        {
            // Input validation check
            if (dto.Quantity <= 0)
            {
                return BadRequest(ApiResponse<string>.FailureResponse("Quantity must be greater than zero."));
            }

            var success = await _purchaseService.ProcessPurchaseAsync(dto);

            if (!success)
            {
                // Note: We return a general failure, but usually this means 
                // either product not found or insufficient bank balance.
                return BadRequest(ApiResponse<string>.FailureResponse("Purchase failed. Please check your bank balance or product details."));
            }

            return Ok(ApiResponse<string>.SuccessResponse("Purchase processed successfully. Inventory and ledger updated."));
        }
        [HttpGet("history/{franchiseId}")]
        public async Task<IActionResult> GetPurchaseHistory(int franchiseId, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            try
            {
                // Agar dates na bheji jayen to default pichle 30 din ka record dikhaye
                if (from == DateTime.MinValue) from = DateTime.Now.AddDays(-30);
                if (to == DateTime.MinValue) to = DateTime.Now;

                var history = await _purchaseService.GetPurchaseHistoryAsync(franchiseId, from, to);

                if (history == null || !history.Any())
                {
                    return Ok(ApiResponse<IEnumerable<PurchaseHistoryDto>>.SuccessResponse(new List<PurchaseHistoryDto>(), "No purchases found for this period."));
                }

                return Ok(ApiResponse<IEnumerable<PurchaseHistoryDto>>.SuccessResponse(history, "Purchase history retrieved successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailureResponse($"Error retrieving purchase history: {ex.Message}"));
            }
        }
    }
}
