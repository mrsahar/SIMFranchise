using Microsoft.AspNetCore.Mvc;
using SIMFranchise.DTOs.Product;
using SIMFranchise.Interfaces;
using SIMFranchise.Wrappers;

[Route("api/[controller]")]
[ApiController]
public class LoadOperatorController : ControllerBase
{
    private readonly ILoadOperatorService _loadService;

    public LoadOperatorController(ILoadOperatorService loadService)
    {
        _loadService = loadService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(LoadOperatorCreateDto dto)
    {
        var success = await _loadService.CreateLoadOperatorAsync(dto);
        if (!success) return BadRequest(ApiResponse<string>.FailureResponse("Failed to register load operator."));
        return Ok(ApiResponse<string>.SuccessResponse(  "Load operator registered successfully."));
    }

    [HttpGet("by-company/{companyId}")]
    public async Task<IActionResult> GetByCompany(int companyId)
    {
        var ops = await _loadService.GetOperatorsByCompanyAsync(companyId);
        return Ok(ApiResponse<object>.SuccessResponse(ops, "Load operators retrieved."));
    }
}