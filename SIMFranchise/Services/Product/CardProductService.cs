using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Product;
using SIMFranchise.Interfaces.Inventory;
using SIMFranchise.Models;

namespace SIMFranchise.Services.Products
{
    public class CardProductService : ICardProductService
    {
        private readonly SimfranchiseManagementDbContext _context;


        public CardProductService(SimfranchiseManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCardProductAsync(CardProductCreateDto dto)
        {
            var card = new CardProduct
            {
                CompanyId = dto.CompanyId,
                CardName = dto.CardName,
                CardValue = dto.CardValue,
                CostPrice = dto.CostPrice,
                SalePrice = dto.SalePrice,
                IsActive = true
            };

            _context.CardProducts.Add(card);
            return await _context.SaveChangesAsync() > 0;
        }

        // Company ke mutabiq cards ki list
        public async Task<List<CardProduct>> GetCardsByCompanyAsync(int companyId)
        {
            return await _context.CardProducts
                .Where(c => c.CompanyId == companyId && c.IsActive == true)
                .ToListAsync();
        } 
        public async Task<bool> UpdateCardProductAsync(long id, CardProductCreateDto dto)
        {
            // 1. Database se purana record dhoondein
            var card = await _context.CardProducts.FindAsync(id);

            if (card == null)
            {
                return false; // Agar card nahi mila to return false
            }

            // 2. Nayi values assign karein
            card.CardName = dto.CardName;
            card.CardValue = dto.CardValue;
            card.CostPrice = dto.CostPrice;
            card.SalePrice = dto.SalePrice;
            card.CompanyId = dto.CompanyId;

            // 3. Database mein save karein
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

