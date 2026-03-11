namespace SIMFranchise.DTOs.Sales
{
    public class SaleCreateDto
    {
        public int FranchiseId { get; set; }
        public int TeamMemberId { get; set; }
        public string ProductType { get; set; } = null!; // SIM, LOAD, CARD
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalAmount { get; set; } // Customer se kitne paise liye
        public string PaymentMode { get; set; } = "CASH"; // CASH ya BANK
        public decimal UnitCostPrice { get; set; }
        public decimal UnitSalePrice { get; set; }
    }
}