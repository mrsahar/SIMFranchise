namespace SIMFranchise.DTOs.Sales
{
    public class FranchiseGrandReportDto
    {
        public string Title { get; set; } = "GRAND TOTAL";
        public decimal TotalSims { get; set; } = 0;
        public decimal TotalLoadAmount { get; set; } = 0;
        public decimal TotalCards { get; set; } = 0;
        public decimal TotalRevenue { get; set; } = 0;
        public decimal TotalNetProfit { get; set; } = 0;
        public int TotalSalesCount { get; set; } = 0;
    }
}