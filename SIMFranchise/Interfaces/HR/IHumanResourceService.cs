using SIMFranchise.DTOs.Auth;
using SIMFranchise.DTOs.HR;
using SIMFranchise.Models;

namespace SIMFranchise.Interfaces
{
    public interface IHumanResourceService
    {
        // Role Management
        Task<bool> CreateRoleAsync(RoleDto dto);
        Task<bool> UpdateRoleAsync(int id, RoleDto dto);
        Task<bool> DeleteRoleAsync(int id);
        Task<IEnumerable<Role>> GetAllRolesAsync();

        // User Management
        Task<bool> CreateUserAsync(UserCreateDto dto);
        Task<bool> UpdateUserAsync(int id, UserCreateDto dto);

        // Team Management
        Task<bool> CreateTeamAsync(TeamCreateDto dto);
        Task<bool> UpdateTeamAsync(int id, TeamCreateDto dto);

        // Team Member Management
        Task<bool> AddMemberToTeamAsync(TeamMemberCreateDto dto);
        Task<bool> UpdateMemberAsync(int id, TeamMemberCreateDto dto);


        Task<IEnumerable<TeamListDto>> GetTeamsByFranchiseAsync(int franchiseId);
        Task<IEnumerable<MemberListDto>> GetMembersByFranchiseAsync(int franchiseId);
        Task<IEnumerable<UserListDto>> GetUsersByFranchiseAsync(int franchiseId);

    }
}