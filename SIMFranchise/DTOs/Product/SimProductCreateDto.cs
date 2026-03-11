namespace SIMFranchise.DTOs.Product
{
    public class SimProductCreateDto
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = null!; // e.g., "Jan 2026 Batch"
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal ActivationTarget { get; set; } // Puray month ka 1 hi target
    }
}