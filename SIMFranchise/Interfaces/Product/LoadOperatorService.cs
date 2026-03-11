using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Product;
using SIMFranchise.Interfaces;
using SIMFranchise.Models;

public class LoadOperatorService : ILoadOperatorService
{
    private readonly SimfranchiseManagementDbContext _context;

    public LoadOperatorService(SimfranchiseManagementDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateLoadOperatorAsync(LoadOperatorCreateDto dto)
    {
        var op = new LoadOperator
        {
            CompanyId = dto.CompanyId,
            OperatorName = dto.OperatorName,
            CommissionPercent = dto.CommissionPercent,
            IsActive = true
        };
        _context.LoadOperators.Add(op);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<LoadOperator>> GetOperatorsByCompanyAsync(int companyId)
    {
        return await _context.LoadOperators
            .Where(o => o.CompanyId == companyId && (o.IsActive ?? false))
            .ToListAsync();
    }

    public async Task<bool> UpdateCommissionAsync(long id, decimal newCommission)
    {
        var op = await _context.LoadOperators.FindAsync(id);
        if (op == null) return false;

        op.CommissionPercent = newCommission;
        return await _context.SaveChangesAsync() > 0;
    }
}