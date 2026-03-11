namespace SIMFranchise.DTOs.Company
{
    public class CompanyUpdateDto
    {
        public string Name { get; set; } = null!;
        public string? Contact { get; set; }
        public bool? IsActive { get; set; }
    }
}
