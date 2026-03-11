using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Sales;
using SIMFranchise.Interfaces;
using SIMFranchise.Models;

namespace SIMFranchise.Services
{
    public class SalesService : ISalesService
    {
        private readonly SimfranchiseManagementDbContext _context;

        public SalesService(SimfranchiseManagementDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ProcessSaleAsync(SaleCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. MEMBER STOCK CHECK  
                // Pehle check karo ke member ke paas waqayi maal hai?
                var mStock = await _context.MemberStocks
                    .FirstOrDefaultAsync(s => s.TeamMemberId == dto.TeamMemberId &&
                                             s.ProductType == dto.ProductType &&
                                             s.ProductId == dto.ProductId);

                if (mStock == null || mStock.CurrentQuantity < dto.Quantity)
                {
                    return false; // Stock nahi hai ya kam hai
                }

                // 2. DEDUCT MEMBER STOCK 
                mStock.CurrentQuantity -= dto.Quantity;
                mStock.LastUpdated = DateTime.Now;

                // 3. UPDATE FRANCHISE BALANCE (CASH IN) 
                var balance = await _context.AccountBalances
                    .FirstOrDefaultAsync(b => b.FranchiseId == dto.FranchiseId &&
                                             b.AccountType == dto.PaymentMode.ToUpper());

                if (balance == null)
                {
                    // Agar account record nahi hai (e.g. pehli sale), to naya banayein
                    balance = new AccountBalance
                    {
                        FranchiseId = dto.FranchiseId,
                        AccountType = dto.PaymentMode.ToUpper(),
                        CurrentBalance = 0,
                        LastUpdated = DateTime.Now
                    };
                    _context.AccountBalances.Add(balance);
                }
                balance.CurrentBalance += dto.TotalAmount;
                balance.LastUpdated = DateTime.Now;

                // 4. CREATE LEDGER TRANSACTION  
                var ledgerEntry = new LedgerTransaction
                {
                    FranchiseId = dto.FranchiseId,
                    AccountType = dto.PaymentMode.ToUpper(),
                    Amount = dto.TotalAmount,
                    Direction = "IN",
                    Source = "SALE",
                    TxnDate = DateOnly.FromDateTime(DateTime.Now),
                    Note = $"Sale by MemberID: {dto.TeamMemberId} - {dto.ProductType}"
                };
                _context.LedgerTransactions.Add(ledgerEntry);

                // 5. RECORD THE SALE  
                var sale = new Sale
                {
                    FranchiseId = dto.FranchiseId,
                    TeamMemberId = dto.TeamMemberId,
                    ProductType = dto.ProductType,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    TotalAmount = dto.TotalAmount,
                    PaymentMode = dto.PaymentMode,
                    SaleDate = DateOnly.FromDateTime(DateTime.Now),
                    UnitCostPrice = dto.UnitCostPrice, // DTO se aayi hui prices
                    UnitSalePrice = dto.UnitSalePrice
                };
                _context.Sales.Add(sale);

                // SAB THEEK HAI? SAVE KARO!  
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<IEnumerable<Sale>> GetMemberDailySalesAsync(int memberId, DateTime date)
        {
            var dateOnly = DateOnly.FromDateTime(date);
            return await _context.Sales
                .Where(s => s.TeamMemberId == memberId && s.SaleDate == dateOnly)
                .ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetFranchiseSalesReportAsync(int franchiseId, DateTime fromDate, DateTime toDate)
        {
            var start = DateOnly.FromDateTime(fromDate);
            var end = DateOnly.FromDateTime(toDate);
            return await _context.Sales
                .Where(s => s.FranchiseId == franchiseId && s.SaleDate >= start && s.SaleDate <= end)
                .OrderByDescending(s => s.SaleDate)
                .ToListAsync();
        }
    }
}