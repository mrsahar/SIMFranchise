namespace SIMFranchise.DTOs.Ledger
{
    public class LedgerHistoryDto
    {
        public long Id { get; set; }
        public string AccountType { get; set; } = null!; // CASH ya BANK
        public string Direction { get; set; } = null!;   // IN ya OUT
        public decimal Amount { get; set; }
        public string? Source { get; set; }              // DEPOSIT, WITHDRAWAL, EXPENSE, SALE
        public DateOnly? TxnDate { get; set; }
        public string? Note { get; set; }                // Detail
    }
}