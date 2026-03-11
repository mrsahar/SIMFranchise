using Microsoft.AspNetCore.Mvc; 
using SIMFranchise.DTOs.Expenses;
using SIMFranchise.Interfaces;
using SIMFranchise.Models;
using SIMFranchise.Wrappers;

namespace SIMFranchise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        // 1. Naya Kharcha Record Karna  
        // POST: api/Expense/add-expense
        [HttpPost("add-expense")]
        public async Task<IActionResult> AddExpense([FromBody] ExpenseCreateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(ApiResponse<string>.FailureResponse("Expense data is required."));
            }

            if (dto.Amount <= 0)
            {
                return BadRequest(ApiResponse<string>.FailureResponse("Amount must be greater than zero."));
            }

            try
            {
                var result = await _expenseService.AddExpenseAsync(dto);

                if (!result)
                {
                    return BadRequest(ApiResponse<string>.FailureResponse("Could not record expense. Please check your inputs."));
                }

                return Ok(ApiResponse<string>.SuccessResponse(  "Expense recorded successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailureResponse($"Internal server error: {ex.Message}"));
            }
        }

        // 2. Kharchon ki History Dekhna (Date Filter ke sath) 
        // GET: api/Expense/history/1?from=2026-03-01&to=2026-03-10
        [HttpGet("history/{franchiseId}")]
        public async Task<IActionResult> GetHistory(int franchiseId, [FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            try
            {
                // Agar dates na bheji jayen, to default pichle 30 din ka data dikhayen
                if (from == DateTime.MinValue) from = DateTime.Now.AddDays(-30);
                if (to == DateTime.MinValue) to = DateTime.Now;

                var history = await _expenseService.GetFranchiseExpensesAsync(franchiseId, from, to);

                if (history == null || !history.Any())
                {
                    return Ok(ApiResponse<List<Expense>>.SuccessResponse(new List<Expense>(), "No expenses found for this period."));
                }

                return Ok(ApiResponse<IEnumerable<Expense>>.SuccessResponse(history, "Expense history retrieved successfully."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.FailureResponse($"Error: {ex.Message}"));
            }
        }
    }
}