using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Auth;
using SIMFranchise.DTOs.HR;
using SIMFranchise.Interfaces;
using SIMFranchise.Models;

namespace SIMFranchise.Services.HR
{
    public class HumanResourceService : IHumanResourceService
    {
        private readonly SimfranchiseManagementDbContext _context;
        public HumanResourceService(SimfranchiseManagementDbContext context) { _context = context; }

        // --- Role Logic ---
        public async Task<bool> CreateRoleAsync(RoleDto dto)
        {
            _context.Roles.Add(new Role { Name = dto.Name });
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateRoleAsync(int id, RoleDto dto)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return false;
            role.Name = dto.Name;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null) return false;
            _context.Roles.Remove(role);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync() => await _context.Roles.ToListAsync();

        // --- User Logic ---
        public async Task<bool> CreateUserAsync(UserCreateDto dto)
        {
            _context.Users.Add(new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password, // Should be hashed in production
                RoleId = dto.RoleId,
                FranchiseId = dto.FranchiseId,
                IsActive = true
            });
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(int id, UserCreateDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            user.Name = dto.Name;
            user.RoleId = dto.RoleId;
            return await _context.SaveChangesAsync() > 0;
        }

        // --- Team & Member Logic ---
        public async Task<bool> CreateTeamAsync(TeamCreateDto dto)
        {
            _context.Teams.Add(new Team { FranchiseId = dto.FranchiseId, Name = dto.Name, Responsibility = dto.Responsibility });
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddMemberToTeamAsync(TeamMemberCreateDto dto)
        {
            _context.TeamMembers.Add(new TeamMember { TeamId = dto.TeamId, Name = dto.Name, BaseSalary = dto.BaseSalary });
            return await _context.SaveChangesAsync() > 0;
        }

        // 1. Team Update Logic
        public async Task<bool> UpdateTeamAsync(int id, TeamCreateDto dto)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) return false;

            team.Name = dto.Name;
            team.Responsibility = dto.Responsibility;
            team.FranchiseId = dto.FranchiseId; // Agar team kisi aur franchise ko shift karni ho

            return await _context.SaveChangesAsync() > 0;
        }

        // 2. Team Member Update Logic
        public async Task<bool> UpdateMemberAsync(int id, TeamMemberCreateDto dto)
        {
            var member = await _context.TeamMembers.FindAsync(id);
            if (member == null) return false;

            member.Name = dto.Name;
            member.TeamId = dto.TeamId; // Member ki team change karne ke liye
            member.BaseSalary = dto.BaseSalary;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
