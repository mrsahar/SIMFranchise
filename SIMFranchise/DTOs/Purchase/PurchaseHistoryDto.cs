namespace SIMFranchise.DTOs.Purchase
{
    public class PurchaseHistoryDto
    {
        public long Id { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = null!; // e.g., Jazz, Telenor
        public string ProductType { get; set; } = null!; // SIM, LOAD, CARD
        public long ProductId { get; set; }

        // Frontend par dikhane ke liye hum naam nikaal lenge
        public string ProductName { get; set; } = null!;

        public decimal Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public DateOnly PurchaseDate { get; set; }
    }
}