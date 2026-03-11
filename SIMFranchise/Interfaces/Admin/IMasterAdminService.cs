using SIMFranchise.DTOs.Admin;
using SIMFranchise.Models;

namespace SIMFranchise.Interfaces
{
    public interface IMasterAdminService
    {
        // Role Control
        Task<bool> UpsertRoleAsync(int? id, MasterRoleDto dto);
        Task<IEnumerable<Role>> GetAllRolesAsync();

        // User Control (System Level)
        Task<bool> CreateUserAsync(MasterUserDto dto);
        Task<bool> ToggleUserStatusAsync(int userId, bool isActive);

        // Global Team & Member Operations
        Task<bool> CreateTeamAsync(MasterTeamDto dto);
        Task<bool> QuickAddMemberAsync(int teamId, string name, decimal salary);
        // Member ko system se khatam karna
        Task<bool> RemoveMemberAsync(int memberId);

        // Member ki team change karna
        Task<bool> ChangeMemberTeamAsync(int memberId, int newTeamId);
    }
}