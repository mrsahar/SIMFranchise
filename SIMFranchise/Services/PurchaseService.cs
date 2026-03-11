using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Purchase;
using SIMFranchise.Interfaces;
using SIMFranchise.Models;

namespace SIMFranchise.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly SimfranchiseManagementDbContext _context;
        public PurchaseService(SimfranchiseManagementDbContext context) {
        
        _context = context;
        
        }
        public async Task<bool> ProcessPurchaseAsync(PurchaseCreateDto dto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Get Product Price (Example for CARD) 
                decimal costPrice = 0;

                if (dto.ProductType == "CARD")
                {
                    var card = await _context.CardProducts
                        .FirstOrDefaultAsync(c => c.Id == dto.ProductId);

                    if (card == null) return false;
                    costPrice = card.CostPrice;
                }
                else if (dto.ProductType == "SIM")
                {
                    // SIM ki logic (Assuming SimProducts table exists)
                    var sim = await _context.SimProducts
                        .FirstOrDefaultAsync(s => s.Id == dto.ProductId);

                    if (sim == null) return false;
                    costPrice = sim.CostPrice;
                }
                else if (dto.ProductType == "LOAD")
                { 
                    costPrice = 1; // Example logic for Load
                }

                decimal totalBill = dto.Quantity * costPrice; 

                // 2. Check Bank Balance (ONLY BANK ALLOWED)  
                var bankBalance = await _context.AccountBalances
                    .FirstOrDefaultAsync(b => b.FranchiseId == dto.FranchiseId && b.AccountType == "BANK");

                if (bankBalance == null || bankBalance.CurrentBalance < totalBill)
                {
                    return false; // Insufficient Funds
                }

                // 3. Create Purchase Record
                var purchase = new Purchase
                {
                    FranchiseId = dto.FranchiseId,
                    CompanyId = dto.CompanyId,
                    ProductType = dto.ProductType,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    TotalAmount = totalBill,
                    PurchaseDate = DateOnly.FromDateTime(DateTime.Now)
                };
                _context.Purchases.Add(purchase);

                // 4. Update Ledger (Money going OUT)  
                var ledgerOut = new LedgerTransaction
                {
                    FranchiseId = dto.FranchiseId,
                    AccountType = "BANK",
                    Amount = totalBill,
                    Direction = "OUT",
                    Source = "PURCHASE",
                    TxnDate = DateOnly.FromDateTime(DateTime.Now),
                    Note = $"Purchased {dto.Quantity} {dto.ProductType} units"
                };
                _context.LedgerTransactions.Add(ledgerOut);

                // 5. Deduct from Account Balance
                bankBalance.CurrentBalance -= totalBill;
                bankBalance.LastUpdated = DateTime.Now;

                // 6. Update Franchise Stock  
                var stock = await _context.FranchiseStocks
                    .FirstOrDefaultAsync(s => s.FranchiseId == dto.FranchiseId &&
                                             s.ProductType == dto.ProductType &&
                                             s.ProductId == dto.ProductId);

                if (stock == null)
                {
                    stock = new FranchiseStock
                    {
                        FranchiseId = dto.FranchiseId,
                        ProductType = dto.ProductType,
                        ProductId = dto.ProductId,
                        Quantity = dto.Quantity
                    };
                    _context.FranchiseStocks.Add(stock);
                }
                else
                {
                    stock.Quantity += dto.Quantity;
                    stock.LastUpdated = DateTime.Now;
                }

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
    }
}
