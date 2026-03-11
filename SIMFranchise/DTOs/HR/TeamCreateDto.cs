namespace SIMFranchise.DTOs.Auth
{
    public class TeamCreateDto
    {
        public int FranchiseId { get; set; }
        public string Name { get; set; } = null!;
        public string Responsibility { get; set; } = null!;
    }
}
