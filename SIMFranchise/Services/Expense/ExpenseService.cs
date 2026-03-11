using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Expenses;
using SIMFranchise.Interfaces;
using SIMFranchise.Models;

namespace SIMFranchise.Services.Expense
{
    public class ExpenseService : IExpenseService
    {
        private readonly SimfranchiseManagementDbContext _context;

        public ExpenseService(SimfranchiseManagementDbContext context)
        {
            _context = context;
        }

        // 1. Add New Expense  
        public async Task<bool> AddExpenseAsync(ExpenseCreateDto dto)
        {
            try
            {
                var expense = new Models.Expense
                {
                    FranchiseId = dto.FranchiseId,
                    Category = dto.Category,
                    Amount = dto.Amount,
                    PaidFrom = dto.PaidFrom.ToUpper(), // CASH or BANK
                    ExpenseDate = DateOnly.FromDateTime(DateTime.Now),
                    Notes = dto.Notes,
                    RecordedBy = dto.RecordedByUserId
                };

                _context.Expenses.Add(expense);

                // Note: Agar aap Balance table update karna chahte hain 
                // to yahan 'PaidFrom' ke mutabiq balance minus karne ki logic aayegi.

                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // 2. Get Expense History  
        public async Task<IEnumerable<Models.Expense>> GetFranchiseExpensesAsync(int franchiseId, DateTime fromDate, DateTime toDate)
        {
            // DateTime ko DateOnly mein convert karna taake comparison sahi ho
            var start = DateOnly.FromDateTime(fromDate);
            var end = DateOnly.FromDateTime(toDate);

            return await _context.Expenses
                .Where(e => e.FranchiseId == franchiseId &&
                            e.ExpenseDate >= start &&
                            e.ExpenseDate <= end)
                .OrderByDescending(e => e.ExpenseDate) // Newest first
                .ThenByDescending(e => e.Id)
                .ToListAsync();
        }
    }
}