namespace SIMFranchise.DTOs.Admin
{
    public class Admin
    {
    }
      // Role Setup
        public class MasterRoleDto { public string Name { get; set; } = null!; }

        // User Setup (Franchise Admin/Manager)
        public class MasterUserDto
        {
            public string Name { get; set; } = null!;
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
            public int RoleId { get; set; }
            public int? FranchiseId { get; set; }
        }

        // Global Team & Member Setup
        public class MasterTeamDto
        {
            public int FranchiseId { get; set; }
            public string Name { get; set; } = null!;
            public string Responsibility { get; set; } = null!;
        }
     
}
