using global::SIMFranchise.DTOs.LedgerTransaction;
using SIMFranchise.DTOs.Ledger;

    namespace SIMFranchise.Interfaces
    {
        public interface ILedgerService
        {
            Task<bool> AddInitialBalanceAsync(LedgerCreateDto dto);
            Task<decimal> GetAccountBalanceAsync(int franchiseId, string accountType);
            Task<LedgerBalanceDto> GetFranchiseSummaryAsync(int franchiseId);
    }
    }
