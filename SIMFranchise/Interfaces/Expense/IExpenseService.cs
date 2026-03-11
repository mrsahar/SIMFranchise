using SIMFranchise.DTOs.Expenses;
using SIMFranchise.Models;

public interface IExpenseService
{
    Task<bool> AddExpenseAsync(ExpenseCreateDto dto); 
    Task<IEnumerable<Expense>> GetFranchiseExpensesAsync(int franchiseId, DateTime fromDate, DateTime toDate);
}