namespace SIMFranchise.DTOs.Company
{
    public class CompanyResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Contact { get; set; }
        public bool? IsActive { get; set; }
    }
}
