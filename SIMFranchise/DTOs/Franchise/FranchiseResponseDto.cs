namespace SIMFranchise.DTOs.Franchise
{
    namespace SIMFranchise.DTOs.Franchise
    {
        public class FranchiseResponseDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string? Location { get; set; }
            public bool Status { get; set; }
            public DateTime? CreatedDate { get; set; }

            // 1. Foreign Key ID: Taake humein pata ho base ID kya hai
            public int CompanyId { get; set; }

            // 2. Company Name (Pro Tip): User ko sirf '1001' dikhana kafi nahi hota,  
            public string? CompanyName { get; set; }
        }
    }
}
