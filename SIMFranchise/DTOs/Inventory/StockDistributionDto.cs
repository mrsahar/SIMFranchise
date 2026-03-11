namespace SIMFranchise.DTOs.Stock
{
    public class StockDistributionDto
    {
        public int FranchiseId { get; set; }
        public int TeamMemberId { get; set; }
        public string ProductType { get; set; } = null!; // SIM, LOAD, CARD
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public int IssuedByUserId { get; set; } // Manager ki ID
    }
}