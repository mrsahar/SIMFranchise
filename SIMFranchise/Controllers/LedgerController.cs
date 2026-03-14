using Microsoft.AspNetCore.Mvc;
using SIMFranchise.DTOs.Ledger;
using SIMFranchise.DTOs.LedgerTransaction;
using SIMFranchise.Interfaces;
using SIMFranchise.Services;
using SIMFranchise.Wrappers;

namespace SIMFranchise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LedgerController : ControllerBase
    {
        private readonly ILedgerService _ledgerService;

        public LedgerController(ILedgerService ledgerService)
        {
            _ledgerService = ledgerService;
        }

        // 💰 Initial Investment ya Deposit jama karne ke liye
        [HttpPost("deposit")]
        public async Task<IActionResult> AddInvestment(LedgerCreateDto dto)
        {
            var success = await _ledgerService.AddInitialBalanceAsync(dto);

            if (!success)
            {
                // English: "Investment could not be processed. Please verify the franchise or the amount."
                return BadRequest(ApiResponse<string>.FailureResponse("Investment could not be processed. Please check the franchise or the amount."));
            }
            // English: "Congratulations! The investment has been successfully recorded."
            return Ok(ApiResponse<string>.SuccessResponse(  "Congratulations! The investment has been successfully recorded."));
        }

        //Franchise ka mojooda balance check karne ke liye
        [HttpGet("balance/{franchiseId}/{accountType}")]
        public async Task<IActionResult> GetBalance(int franchiseId, string accountType)
        {
            // accountType should be 'CASH' or 'BANK'
            var balance = await _ledgerService.GetAccountBalanceAsync(franchiseId, accountType.ToUpper());

            return Ok(ApiResponse<decimal>.SuccessResponse(balance, $"{accountType} balance fetched."));
        }
        [HttpGet("summary/{franchiseId}")]
        public async Task<IActionResult> GetSummary(int franchiseId)
        {
            var summary = await _ledgerService.GetFranchiseSummaryAsync(franchiseId);
            return Ok(ApiResponse<LedgerBalanceDto>.SuccessResponse(summary, "Franchise funds summary fetched."));
        } 

        [HttpPost("withdraw")]
            public async Task<IActionResult> WithdrawFunds([FromBody] LedgerWithdrawDto dto)
            {
                if (dto == null || dto.Amount <= 0)
                {
                    return BadRequest(ApiResponse<string>.FailureResponse("Invalid amount. Amount must be greater than zero."));
                }

                try
                {
                    var success = await _ledgerService.WithdrawAsync(dto);

                    if (!success)
                    {
                        // Ye error tab aayega jab account mein paise na hon
                        return BadRequest(ApiResponse<string>.FailureResponse("Withdrawal failed. Insufficient balance or invalid account."));
                    }

                    return Ok(ApiResponse<string>.SuccessResponse($"Successfully withdrawn {dto.Amount} from {dto.AccountType}."));
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ApiResponse<string>.FailureResponse($"Error processing withdrawal: {ex.Message}"));
                }
            }

        }
}
