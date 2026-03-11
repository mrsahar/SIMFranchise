namespace SIMFranchise.DTOs.Franchise
{
    public class FranchiseCreateDto
    {
        public string Name { get; set; } = null!;
        public string? Location { get; set; }
        public bool Status { get; set; }
        public int CompanyId { get; set; }  
    }
}
