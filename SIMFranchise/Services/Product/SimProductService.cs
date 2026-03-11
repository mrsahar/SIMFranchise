using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Product;
using SIMFranchise.Interfaces;
using SIMFranchise.Models;

namespace SIMFranchise.Services
{
    public class SimProductService : ISimProductService
    {
        private readonly SimfranchiseManagementDbContext _context;

        public SimProductService(SimfranchiseManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateSimBatchAsync(SimProductCreateDto dto)
        {
            var simProduct = new SimProduct
            {
                CompanyId = dto.CompanyId,
                Name = dto.Name,
                CostPrice = dto.CostPrice,
                SalePrice = dto.SalePrice,
                ActivationTarget = dto.ActivationTarget,
                IsActive = true
            };

            _context.SimProducts.Add(simProduct);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<SimProduct>> GetBatchesByCompanyAsync(int companyId)
        {
            return await _context.SimProducts
                .Where(p => p.CompanyId == companyId && (p.IsActive ?? false))
                .OrderByDescending(p => p.Id)
                .ToListAsync();
        }

        public async Task<SimProduct?> GetBatchByIdAsync(long id)
        {
            return await _context.SimProducts.FindAsync(id);
        }

        public async Task<bool> UpdateSimBatchAsync(long id, SimProductCreateDto dto)
        {
            var existingBatch = await _context.SimProducts.FindAsync(id);
            if (existingBatch == null) return false;

            existingBatch.Name = dto.Name;
            existingBatch.CostPrice = dto.CostPrice;
            existingBatch.SalePrice = dto.SalePrice;
            existingBatch.ActivationTarget = dto.ActivationTarget;
            existingBatch.CompanyId = dto.CompanyId;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}