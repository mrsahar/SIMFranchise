namespace SIMFranchise.DTOs.Reports
{
    public class MemberSaleReportDto
    {
        public string MemberName { get; set; } = null!;
        public decimal TotalSims { get; set; }
        public decimal TotalLoadAmount { get; set; }
        public decimal TotalCards { get; set; }
        public decimal TotalCashCollected { get; set; }
        public decimal TotalProfitGenerated { get; set; }
    }
}