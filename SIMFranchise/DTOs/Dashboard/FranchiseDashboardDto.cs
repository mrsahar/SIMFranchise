namespace SIMFranchise.DTOs.Dashboard
{
    public class FranchiseDashboardDto
    {
        public SummaryDto Summary { get; set; } = new();
        public List<StockBreakdownDto> MainStock { get; set; } = new();
        public List<MemberStockDto> MemberStocks { get; set; } = new();
    }

    public class SummaryDto
    {
        public decimal TodayGrossProfit { get; set; }
        public decimal TodayExpenses { get; set; }
        public decimal TodayNetProfit { get; set; }
        public decimal CashInHand { get; set; }
        public decimal BankAccountBalance { get; set; }
        public int TodaySalesCount { get; set; }
    }

    public class StockBreakdownDto
    {
        public string ProductType { get; set; } = null!;
        public decimal TotalInHand { get; set; }
    }

    public class MemberStockDto
    {
        public string MemberName { get; set; } = null!;
        public string ProductType { get; set; } = null!;
        public decimal MemberQuantity { get; set; }
    }
}