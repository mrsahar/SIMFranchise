using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Stock;
using SIMFranchise.Interfaces;
using SIMFranchise.Models;

namespace SIMFranchise.Services
{
    public class DistributionService //: IDistributionService
    {
        private readonly SimfranchiseManagementDbContext _context;

        public DistributionService(SimfranchiseManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IssueStockToMemberAsync(StockDistributionDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // A. Check Franchise Stock
                var fStock = await _context.FranchiseStocks
                    .FirstOrDefaultAsync(s => s.FranchiseId == dto.FranchiseId &&
                                             s.ProductType == dto.ProductType &&
                                             s.ProductId == dto.ProductId);

                if (fStock == null || fStock.Quantity < dto.Quantity) return false;

                // B. Minus from Franchise Stock
                fStock.Quantity -= dto.Quantity;

                // C. Add to Member Stock
                var mStock = await _context.MemberStocks
                    .FirstOrDefaultAsync(s => s.TeamMemberId == dto.TeamMemberId &&
                                             s.ProductType == dto.ProductType &&
                                             s.ProductId == dto.ProductId);

                if (mStock == null)
                {
                    _context.MemberStocks.Add(new MemberStock
                    {
                        FranchiseId = dto.FranchiseId,
                        TeamMemberId = dto.TeamMemberId,
                        ProductType = dto.ProductType,
                        ProductId = dto.ProductId,
                        CurrentQuantity = dto.Quantity,
                        LastUpdated = DateTime.Now
                    });
                }
                else
                {
                    mStock.CurrentQuantity += dto.Quantity;
                    mStock.LastUpdated = DateTime.Now;
                }

                // D. Log the transaction (ISSUE)
                _context.DistributionLogs.Add(new DistributionLog
                {
                    FranchiseId = dto.FranchiseId,
                    TeamMemberId = dto.TeamMemberId,
                    ProductType = dto.ProductType,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    EntryType = "ISSUE",
                    IssuedBy = dto.IssuedByUserId,
                    LogDate = DateTime.Now
                });

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> ReturnStockToFranchiseAsync(StockDistributionDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // A. Check Member Stock (Kya member ke pas itna maal hai bhi?)
                var mStock = await _context.MemberStocks
                    .FirstOrDefaultAsync(s => s.TeamMemberId == dto.TeamMemberId &&
                                             s.ProductType == dto.ProductType &&
                                             s.ProductId == dto.ProductId);

                if (mStock == null || mStock.CurrentQuantity < dto.Quantity) return false;

                // B. Minus from Member Stock
                mStock.CurrentQuantity -= dto.Quantity;

                // C. Add Back to Franchise Stock
                var fStock = await _context.FranchiseStocks
                    .FirstOrDefaultAsync(s => s.FranchiseId == dto.FranchiseId &&
                                             s.ProductType == dto.ProductType &&
                                             s.ProductId == dto.ProductId);

                if (fStock != null) fStock.Quantity += dto.Quantity;

                // D. Log the transaction (RETURN)
                _context.DistributionLogs.Add(new DistributionLog
                {
                    FranchiseId = dto.FranchiseId,
                    TeamMemberId = dto.TeamMemberId,
                    ProductType = dto.ProductType,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    EntryType = "RETURN",
                    IssuedBy = dto.IssuedByUserId,
                    LogDate = DateTime.Now
                });

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<IEnumerable<MemberStock>> GetMemberCurrentStockAsync(int memberId)
        {
            return await _context.MemberStocks
                .Where(ms => ms.TeamMemberId == memberId && ms.CurrentQuantity > 0)
                .ToListAsync();
        }

        public async Task<IEnumerable<DistributionLog>> GetDistributionLogsAsync(int franchiseId)
        {
            return await _context.DistributionLogs
                .Where(l => l.FranchiseId == franchiseId)
                .OrderByDescending(l => l.LogDate)
                .ToListAsync();
        }
    }
}