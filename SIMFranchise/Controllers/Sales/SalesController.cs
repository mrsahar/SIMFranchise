using Microsoft.AspNetCore.Mvc; 
using SIMFranchise.DTOs.Sales;
using SIMFranchise.Interfaces;
using SIMFranchise.Wrappers;

namespace SIMFranchise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        // 1. Nayi Sale record karna 🛒
        [HttpPost("process-sale")]
        public async Task<IActionResult> CreateSale([FromBody] SaleCreateDto dto)
        {
            var success = await _salesService.ProcessSaleAsync(dto);
            if (!success)
            {
                // Agar stock kam ho ya koi aur masla ho
                return BadRequest(ApiResponse<string>.FailureResponse("Sale failed. Please check if the member has sufficient stock."));
            }

            return Ok(ApiResponse<string>.SuccessResponse("Sale recorded successfully and stock updated."));
        }

        // 2. Kisi Member ki aaj ki performance dekhna 📊
        [HttpGet("member-daily-sales/{memberId}")]
        public async Task<IActionResult> GetMemberSales(int memberId)
        {
            var sales = await _salesService.GetMemberDailySalesAsync(memberId, DateTime.Now);
            return Ok(ApiResponse<object>.SuccessResponse(sales, "Daily sales retrieved."));
        }

        // 3. Franchise ki report (Date Range ke mutabiq) 📅
        [HttpGet("franchise-report/{franchiseId}")]
        public async Task<IActionResult> GetFranchiseReport(int franchiseId, [FromQuery] DateTime fromDate, [FromQuery] DateTime toDate)
        {
            var report = await _salesService.GetFranchiseSalesReportAsync(franchiseId, fromDate, toDate);
            return Ok(ApiResponse<object>.SuccessResponse(report, "Sales report generated successfully."));
        }
    }
}