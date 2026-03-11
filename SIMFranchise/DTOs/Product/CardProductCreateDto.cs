namespace SIMFranchise.DTOs.Product
{
    public class CardProductCreateDto
    {
        public int CompanyId { get; set; } // Jazz, Telenor etc.
        public string CardName { get; set; } = null!; // e.g., "Super Card 2000"
        public int CardValue { get; set; } // Face value e.g., 2000
        public decimal CostPrice { get; set; } // Franchise ko kitne ka parta hai
        public decimal SalePrice { get; set; } // Customer ko kitne ka bechna hai
    }
}
