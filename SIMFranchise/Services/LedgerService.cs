using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Ledger;
using SIMFranchise.DTOs.LedgerTransaction;
using SIMFranchise.Interfaces;
using SIMFranchise.Models; 

namespace SIMFranchise.Services
{
    public class LedgerService : ILedgerService
    {
        private readonly SimfranchiseManagementDbContext _context;

        public LedgerService(SimfranchiseManagementDbContext context)
        {
            _context = context;
        }

        //Account ka balance check karne ke liye
        public async Task<decimal> GetAccountBalanceAsync(int franchiseId, string accountType)
        {
            var transactions = await _context.LedgerTransactions
                .Where(t => t.FranchiseId == franchiseId && t.AccountType == accountType)
                .ToListAsync();

            var income = transactions.Where(t => t.Direction == "IN").Sum(t => t.Amount);
            var expense = transactions.Where(t => t.Direction == "OUT").Sum(t => t.Amount);

            return income - expense;
        }

        // Nayi Entry (Investment/Deposit) karne ke liye
        public async Task<bool> AddInitialBalanceAsync(LedgerCreateDto dto)
        {
            // 1. Transaction Shuru karein 🛡️
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // A. Ledger mein record add karein
                var ledgerEntry = new LedgerTransaction
                {
                    FranchiseId = dto.FranchiseId,
                    AccountType = dto.AccountType.ToUpper(),
                    Amount = dto.Amount,
                    Direction = "IN",
                    Source = "INITIAL_INVESTMENT",
                    TxnDate = DateOnly.FromDateTime(DateTime.Now),
                    Note = dto.Note
                };
                _context.LedgerTransactions.Add(ledgerEntry);

                // B. AccountBalance table update karein 📈
                var balance = await _context.AccountBalances
                    .FirstOrDefaultAsync(b => b.FranchiseId == dto.FranchiseId && b.AccountType == dto.AccountType.ToUpper());

                if (balance == null)
                {
                    // Pehli baar investment aa rahi hai to naya record banayein
                    balance = new AccountBalance
                    {
                        FranchiseId = dto.FranchiseId,
                        AccountType = dto.AccountType.ToUpper(),
                        CurrentBalance = dto.Amount,
                        LastUpdated = DateTime.Now
                    };
                    _context.AccountBalances.Add(balance);
                }
                else
                {
                    // Purane balance mein plus karein
                    balance.CurrentBalance += dto.Amount;
                    balance.LastUpdated = DateTime.Now;
                }

                // C. Dono tables ko save karein
                await _context.SaveChangesAsync();

                // D. Sab theek raha to Commit karein (Yaani pakka save) ✅
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                // Agar kahin bhi error aaya, to Rollback (Sab kuch wapas purani halat mein) ❌
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<LedgerBalanceDto> GetFranchiseSummaryAsync(int franchiseId)
        {
            var balances = await _context.AccountBalances
                .Where(b => b.FranchiseId == franchiseId)
                .ToListAsync();

            return new LedgerBalanceDto
            {
                CashBalance = balances.FirstOrDefault(b => b.AccountType == "CASH")?.CurrentBalance ?? 0,
                BankBalance = balances.FirstOrDefault(b => b.AccountType == "BANK")?.CurrentBalance ?? 0
            };
        }
    }
}