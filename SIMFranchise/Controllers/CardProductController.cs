using Microsoft.AspNetCore.Mvc;
using SIMFranchise.Wrappers;
using SIMFranchise.DTOs.Product; 
using SIMFranchise.Interfaces.Inventory;
using SIMFranchise.Models; 

namespace SIMFranchise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardProductController : ControllerBase
    {
        private readonly ICardProductService _cardService;

        public CardProductController(ICardProductService cardService)
        {
            _cardService = cardService;
        }

        // 1. Naya Card Product Create Karna (e.g. Upower 350)
        [HttpPost]
        public async Task<IActionResult> Create(CardProductCreateDto dto)
        {
            var success = await _cardService.CreateCardProductAsync(dto);
            if (!success)
            {
                // English: "Failed to create the card." ya "Card could not be created."
                return BadRequest(ApiResponse<string>.FailureResponse("Failed to create the card."));
            } 
            // English: "Card product added successfully!"
            return Ok(ApiResponse<string>.SuccessResponse("Card product has been successfully added!"));
        }

        // 2. Kisi specific Company ke saare cards dekhna (Dropdown ke liye)
        [HttpGet("company/{companyId}")]
        public async Task<IActionResult> GetByCompany(int companyId)
        {
            var cards = await _cardService.GetCardsByCompanyAsync(companyId);
            return Ok(ApiResponse<List<CardProduct>>.SuccessResponse(cards, "Cards fetched successfully."));
        }

        // 3. Card ki details update karna (Price change karne ke liye)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, CardProductCreateDto dto)
        {
            var success = await _cardService.UpdateCardProductAsync(id, dto);
            if (!success)
            {
                // English: "Card not found or update failed."
                return NotFound(ApiResponse<string>.FailureResponse("Card not found or the update failed."));
            } 
            // English: "Card details updated successfully."
            return Ok(ApiResponse<string>.SuccessResponse("Card details have been successfully updated."));
        }
    }
}