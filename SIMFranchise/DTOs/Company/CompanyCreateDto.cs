namespace SIMFranchise.DTOs.Company
{
    public class CompanyCreateDto
    {
        public string Name { get; set; } = null!;
        public string? Contact { get; set; }
        public bool? IsActive { get; set; }
    }
}

