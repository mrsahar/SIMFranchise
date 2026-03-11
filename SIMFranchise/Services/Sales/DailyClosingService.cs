using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Closing;
using SIMFranchise.Interfaces.Sales;
using SIMFranchise.Models;

namespace SIMFranchise.Services.Sales
{
    public class DailyClosingService : IDailyClosingService
    {
        private readonly SimfranchiseManagementDbContext _context;

        public DailyClosingService(SimfranchiseManagementDbContext context)
        {
            _context = context;
        }
        public async Task<bool> PerformDailyClosingAsync(DailyClosingCreateDto dto)
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

          var alreadyClosed = await _context.DailyClosings
        .AnyAsync(c => c.FranchiseId == dto.FranchiseId && c.ClosingDate == today);

            if (alreadyClosed) return false;

            // 1. Aaj ki total sale calculate karo system se
            var systemTotal = await _context.Sales
                .Where(s => s.FranchiseId == dto.FranchiseId && s.SaleDate == today)
                .SumAsync(s => s.TotalAmount);

            // 2. Record save karo
            var closing = new DailyClosing
            {
                FranchiseId = dto.FranchiseId,
                ClosingDate = today,
                SystemCash = systemTotal,
                ActualCash = dto.ActualCash,
                ClosedBy = dto.ClosedByUserId,
                Notes = dto.Notes
            };

            _context.DailyClosings.Add(closing);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<IEnumerable<DailyClosing>> GetClosingHistoryAsync(int franchiseId)
        {
            return await _context.DailyClosings
                .Where(c => c.FranchiseId == franchiseId)
                .OrderByDescending(c => c.ClosingDate) // Taake aaj ki closing sab se ooper ho
                .Take(30) // Sirf pichle 30 din ka record (aap isay change bhi kar sakte hain)
                .ToListAsync();
        }
    }
}
