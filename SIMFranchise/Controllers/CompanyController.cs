using Microsoft.AspNetCore.Mvc;
using SIMFranchise.DTOs.Company;
using SIMFranchise.Interfaces;
using SIMFranchise.Wrappers;

namespace SIMFranchise.Controllers
{
    [Route("api/[controller]")] // Iska matlab hai URL hoga: api/company
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        // Hum Service ko yahan inject kar rahe hain
        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }
         
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var companies = await _companyService.GetAllCompaniesAsync();

            // SuccessResponse use karte hue data bhejien
            var response = ApiResponse<List<CompanyResponseDto>>.SuccessResponse(companies, "Companies fetched successfully");

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);

            if (company == null)
            {
                return NotFound(ApiResponse<CompanyResponseDto>.FailureResponse("Company not found."));
            }

            var response = ApiResponse<CompanyResponseDto>.SuccessResponse(company, "Company detail found.");
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyCreateDto dto)
        {
            var result = await _companyService.CreateCompanyAsync(dto);
            // CreatedAtAction user ko batata hai ke naya resource kahan bana hai
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CompanyUpdateDto dto)
        {
            var success = await _companyService.UpdateCompanyAsync(id, dto);
            if (!success) return NotFound();

            return NoContent(); // 204 update ke baad koi data wapis bhejne ki zaroorat nahi
        }


    }
}