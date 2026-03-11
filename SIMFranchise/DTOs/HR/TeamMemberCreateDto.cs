namespace SIMFranchise.DTOs.Auth
{
    public class TeamMemberCreateDto
    {
        public int TeamId { get; set; }
        public string Name { get; set; } = null!;
        public decimal BaseSalary { get; set; }
    }
}
