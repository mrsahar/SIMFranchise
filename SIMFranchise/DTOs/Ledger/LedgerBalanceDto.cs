namespace SIMFranchise.DTOs.Ledger
{
    public class LedgerBalanceDto
    {
        public decimal CashBalance { get; set; }
        public decimal BankBalance { get; set; }
        public decimal TotalBalance => CashBalance + BankBalance; // Dono ka total automatic ho jaye ga
    }
}