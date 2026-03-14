namespace SIMFranchise.DTOs.HR
{
    // Team Dropdown ke liye
    public class TeamListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Responsibility { get; set; }
    }

    // Member Dropdown ke liye (With Team Name)
    public class MemberListDto
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; }
    }

    // User Dropdown/List ke liye
    public class UserListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool IsActive { get; set; }
    }
}