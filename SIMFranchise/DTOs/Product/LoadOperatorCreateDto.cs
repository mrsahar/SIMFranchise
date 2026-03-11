namespace SIMFranchise.DTOs.Product
{
    public class LoadOperatorCreateDto
    {
        public int CompanyId { get; set; }
        public string OperatorName { get; set; } = null!; // e.g., "Jazz Load"
        public decimal CommissionPercent { get; set; }  // e.g., 2.0 (matlab 2%)
    }
}