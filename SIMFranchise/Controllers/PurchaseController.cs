using Microsoft.AspNetCore.Mvc;
 
using SIMFranchise.DTOs.Purchase;
using SIMFranchise.Interfaces;
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
    }
}
