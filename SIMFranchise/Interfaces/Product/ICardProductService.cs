using SIMFranchise.DTOs.Product;
using SIMFranchise.Models;

namespace SIMFranchise.Interfaces.Inventory
{
    public interface ICardProductService
    {
        Task<bool> CreateCardProductAsync(CardProductCreateDto dto);
        Task<List<CardProduct>> GetCardsByCompanyAsync(int companyId);
        Task<bool> UpdateCardProductAsync(long id, CardProductCreateDto dto);
    }
}
