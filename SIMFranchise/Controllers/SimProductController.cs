using Microsoft.AspNetCore.Mvc; 
using SIMFranchise.DTOs.Product;
using SIMFranchise.Interfaces;
using SIMFranchise.Wrappers;

namespace SIMFranchise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SimProductController : ControllerBase
    {
        private readonly ISimProductService _simService;

        public SimProductController(ISimProductService simService)
        {
            _simService = simService;
        }

        [HttpPost("create-batch")]
        public async Task<IActionResult> CreateBatch(SimProductCreateDto dto)
        {
            var success = await _simService.CreateSimBatchAsync(dto);
            if (!success) return BadRequest(ApiResponse<string>.FailureResponse("Failed to create SIM batch."));
            return Ok(ApiResponse<string>.SuccessResponse( "SIM batch created successfully."));
        }

        [HttpGet("by-company/{companyId}")]
        public async Task<IActionResult> GetByCompany(int companyId)
        {
            var batches = await _simService.GetBatchesByCompanyAsync(companyId);
            return Ok(ApiResponse<object>.SuccessResponse(batches, "Batches retrieved successfully."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var batch = await _simService.GetBatchByIdAsync(id);
            if (batch == null) return NotFound(ApiResponse<string>.FailureResponse("Batch not found."));
            return Ok(ApiResponse<object>.SuccessResponse(batch, "Batch details retrieved."));
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long id, SimProductCreateDto dto)
        {
            var success = await _simService.UpdateSimBatchAsync(id, dto);
            if (!success) return NotFound(ApiResponse<string>.FailureResponse("Update failed. Batch not found."));
            return Ok(ApiResponse<string>.SuccessResponse(  "Batch updated successfully."));
        }
    }
}