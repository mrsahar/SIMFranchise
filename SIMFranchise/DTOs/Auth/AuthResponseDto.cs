namespace SIMFranchise.DTOs.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = null!; // Angular isko save karega
        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public int RoleId { get; set; }
        public int? FranchiseId { get; set; }
    }
}
