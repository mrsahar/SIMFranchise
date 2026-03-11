using Microsoft.EntityFrameworkCore;
using SIMFranchise.Data;
using SIMFranchise.DTOs.Admin;
using SIMFranchise.Interfaces;
using SIMFranchise.Models;

public class MasterAdminService : IMasterAdminService
{
    private readonly SimfranchiseManagementDbContext _context;
    public MasterAdminService(SimfranchiseManagementDbContext context) { _context = context; }

    public async Task<bool> UpsertRoleAsync(int? id, MasterRoleDto dto)
    {
        if (id.HasValue)
        {
            var role = await _context.Roles.FindAsync(id.Value);
            if (role != null) role.Name = dto.Name;
        }
        else
        {
            _context.Roles.Add(new Role { Name = dto.Name });
        }
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> CreateUserAsync(MasterUserDto dto)
    {
        _context.Users.Add(new User
        {
            Name = dto.Name,
            Email = dto.Email,
            PasswordHash = dto.Password, // Production mein hash kariyega
            RoleId = dto.RoleId,
            FranchiseId = dto.FranchiseId,
            IsActive = true
        });
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> CreateTeamAsync(MasterTeamDto dto)
    {
        _context.Teams.Add(new Team { FranchiseId = dto.FranchiseId, Name = dto.Name, Responsibility = dto.Responsibility });
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> QuickAddMemberAsync(int teamId, string name, decimal salary)
    {
        _context.TeamMembers.Add(new TeamMember { TeamId = teamId, Name = name, BaseSalary = salary });
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync() => await _context.Roles.ToListAsync();

    public async Task<bool> ToggleUserStatusAsync(int userId, bool isActive)
    {
        var user = await _context.Users.FindAsync(userId);
        if (user == null) return false;
        user.IsActive = isActive;
        return await _context.SaveChangesAsync() > 0;
    }
    public async Task<bool> RemoveMemberAsync(int memberId)
    {
        var member = await _context.TeamMembers.FindAsync(memberId);
        if (member == null) return false;

        // Option A: Hard Delete (Table se khatam kar dena)
        _context.TeamMembers.Remove(member);

        // Note: Agar aap history rakhna chahte hain to 'IsActive = false' wala column add kar ke soft delete karein.

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ChangeMemberTeamAsync(int memberId, int newTeamId)
    {
        var member = await _context.TeamMembers.FindAsync(memberId);
        var teamExists = await _context.Teams.AnyAsync(t => t.Id == newTeamId);

        if (member == null || !teamExists) return false;

        // Team ID update kar di
        member.TeamId = newTeamId;

        return await _context.SaveChangesAsync() > 0;
    }
}