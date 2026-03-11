namespace SIMFranchise.DTOs.Expenses
{
    public class ExpenseCreateDto
    {
        public int FranchiseId { get; set; }
        public string Category { get; set; } = null!; // e.g., Rent, Petrol, Tea, Bill
        public decimal Amount { get; set; }
        public string PaidFrom { get; set; } = "CASH"; // CASH ya BANK
        public string? Notes { get; set; }
        public int RecordedByUserId { get; set; } // Kis manager ne entry ki
    }
}