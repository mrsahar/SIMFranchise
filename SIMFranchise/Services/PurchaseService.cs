using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Purchase;
using SIMFranchise.Interfaces.Purchase;
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
        public async Task<IEnumerable<PurchaseHistoryDto>> GetPurchaseHistoryAsync(int franchiseId, DateTime fromDate, DateTime toDate)
        {
            var start = DateOnly.FromDateTime(fromDate);
            var end = DateOnly.FromDateTime(toDate);

            // 1. Pehle database se raw purchases mangwayein Company ke data ke sath
            var purchases = await _context.Purchases
                .Where(p => p.FranchiseId == franchiseId &&
                            p.PurchaseDate >= start &&
                            p.PurchaseDate <= end)
                .OrderByDescending(p => p.PurchaseDate)
                .ToListAsync();

            // Companies ki list (Names ke liye)
            var companies = await _context.Companies.ToDictionaryAsync(c => c.Id, c => c.Name);

            var historyList = new List<PurchaseHistoryDto>();

            // 2. Data ko DTO mein map karein aur Product ka naam banayein
            foreach (var p in purchases)
            {
                string compName = companies.ContainsKey(p.CompanyId) ? companies[p.CompanyId] : "Unknown";
                string prodName = $"{compName} {p.ProductType}"; // Default name (e.g., "Jazz LOAD")

                // Agar aap chahein to yahan SimProducts ya CardProducts se exact naam bhi nikaal sakte hain
                // Abhi ke liye hum Company + Type ko use kar rahe hain jo sab se best hai

                historyList.Add(item: new PurchaseHistoryDto
                {
                    Id = p.Id,
                    CompanyId = p.CompanyId,
                    CompanyName = compName,
                    ProductType = p.ProductType!,
                    ProductId = p.ProductId,
                    ProductName = prodName,
                    Quantity = p.Quantity,
                    TotalAmount = p.TotalAmount,
                    PurchaseDate = p.PurchaseDate
                });
            }

            return historyList;
        }
    }
}
