namespace SIMFranchise.DTOs.Purchase
{
    public class PurchaseCreateDto
    {
        public int FranchiseId { get; set; }
        public int CompanyId { get; set; }
        public required string ProductType { get; set; } // "CARD", "SIM", or "LOAD"
        public int ProductId { get; set; }     // ID of CardProduct or SimProduct
        public decimal Quantity { get; set; }
        public string? Note { get; set; }
    }
}