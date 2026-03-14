namespace SIMFranchise.DTOs.Ledger
{
    public class LedgerWithdrawDto
    {
        public int FranchiseId { get; set; }
        public string AccountType { get; set; } = null!; // "CASH" ya "BANK"
        public decimal Amount { get; set; }
        public string? Note { get; set; } // Kis maqsad se nikale? e.g., "Owner ne zati use ke liye nikale"
    }
}