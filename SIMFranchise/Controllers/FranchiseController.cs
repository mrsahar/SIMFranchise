using Microsoft.AspNetCore.Mvc;
namespace SIMFranchise.Controllers
{
    using global::SIMFranchise.DTOs.Franchise;
    using global::SIMFranchise.DTOs.Franchise.SIMFranchise.DTOs.Franchise;
    using global::SIMFranchise.Interfaces;
    using global::SIMFranchise.Wrappers;
    using Microsoft.AspNetCore.Mvc; 

    namespace SIMFranchise.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class FranchiseController : ControllerBase
        {
            private readonly IFranchiseService _franchiseService;

            public FranchiseController(IFranchiseService franchiseService)
            {
                _franchiseService = franchiseService;
            }

            [HttpGet]
            public async Task<IActionResult> GetAll()
            {
                var franchises = await _franchiseService.GetAllFranchisesAsync();
                var response = ApiResponse<List<FranchiseResponseDto>>.SuccessResponse(franchises, "Franchises fetched successfully");
                return Ok(response);
            }

            [HttpGet("{id}")] 
            public async Task<IActionResult> GetById(int id)
            {
                var franchise = await _franchiseService.GetFranchiseByIdAsync(id);

                if (franchise == null)
                { 
                    return NotFound(ApiResponse<FranchiseResponseDto>.FailureResponse("Franchise not found."));
                } 
                return Ok(ApiResponse<FranchiseResponseDto>.SuccessResponse(franchise, "Franchise Found Successfully."));
            }

            [HttpPost]
            public async Task<IActionResult> Create(FranchiseCreateDto dto)
            { 
                var result = await _franchiseService.CreateFranchiseAsync(dto); 
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, FranchiseUpdateDto dto)
            {
                var success = await _franchiseService.UpdateFranchiseAsync(id, dto);
                if (!success) return NotFound();

                return NoContent();
            }
        }
    }
}
