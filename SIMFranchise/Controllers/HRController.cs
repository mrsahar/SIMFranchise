using Microsoft.AspNetCore.Mvc; 
using SIMFranchise.DTOs.Auth;
using SIMFranchise.DTOs.HR;
using SIMFranchise.Interfaces;
using SIMFranchise.Wrappers;

namespace SIMFranchise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HRController : ControllerBase
    {
        private readonly IHumanResourceService _hrService;

        public HRController(IHumanResourceService hrService)
        {
            _hrService = hrService;
        }

        // =================  ROLE MANAGEMENT  =================

        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole(RoleDto dto)
        {
            var success = await _hrService.CreateRoleAsync(dto);
            if (!success) return BadRequest(ApiResponse<string>.FailureResponse("Failed to create role."));
            return Ok(ApiResponse<string>.SuccessResponse(null, "Role created successfully."));
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _hrService.GetAllRolesAsync();
            return Ok(ApiResponse<object>.SuccessResponse(roles, "Roles retrieved successfully."));
        }

        [HttpPut("roles/{id}")]
        public async Task<IActionResult> UpdateRole(int id, RoleDto dto)
        {
            var success = await _hrService.UpdateRoleAsync(id, dto);
            if (!success) return NotFound(ApiResponse<string>.FailureResponse("Role not found."));
            return Ok(ApiResponse<string>.SuccessResponse(null, "Role updated successfully."));
        }

        [HttpDelete("roles/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var success = await _hrService.DeleteRoleAsync(id);
            if (!success) return BadRequest(ApiResponse<string>.FailureResponse("Cannot delete role. It might be in use."));
            return Ok(ApiResponse<string>.SuccessResponse(null, "Role deleted successfully."));
        }

        // =================  USER MANAGEMENT  =================

        [HttpPost("users")]
        public async Task<IActionResult> CreateUser(UserCreateDto dto)
        {
            var success = await _hrService.CreateUserAsync(dto);
            if (!success) return BadRequest(ApiResponse<string>.FailureResponse("Failed to create user. Email might already exist."));
            return Ok(ApiResponse<string>.SuccessResponse(null, "User created successfully."));
        }

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserCreateDto dto)
        {
            var success = await _hrService.UpdateUserAsync(id, dto);
            if (!success) return NotFound(ApiResponse<string>.FailureResponse("User not found."));
            return Ok(ApiResponse<string>.SuccessResponse(null, "User updated successfully."));
        }

        // =================  TEAM MANAGEMENT  =================

        [HttpPost("teams")]
        public async Task<IActionResult> CreateTeam(TeamCreateDto dto)
        {
            var success = await _hrService.CreateTeamAsync(dto);
            if (!success) return BadRequest(ApiResponse<string>.FailureResponse("Failed to create team."));
            return Ok(ApiResponse<string>.SuccessResponse(null, "Team created successfully."));
        }

        [HttpPut("teams/{id}")]
        public async Task<IActionResult> UpdateTeam(int id, TeamCreateDto dto)
        {
            var success = await _hrService.UpdateTeamAsync(id, dto);
            if (!success) return NotFound(ApiResponse<string>.FailureResponse("Team not found."));
            return Ok(ApiResponse<string>.SuccessResponse(null, "Team updated successfully."));
        }

        // =================  TEAM MEMBER MANAGEMENT  =================

        [HttpPost("members")]
        public async Task<IActionResult> AddMember(TeamMemberCreateDto dto)
        {
            var success = await _hrService.AddMemberToTeamAsync(dto);
            if (!success) return BadRequest(ApiResponse<string>.FailureResponse("Failed to add team member."));
            return Ok(ApiResponse<string>.SuccessResponse(null, "Team member added successfully."));
        }

        [HttpPut("members/{id}")]
        public async Task<IActionResult> UpdateMember(int id, TeamMemberCreateDto dto)
        {
            var success = await _hrService.UpdateMemberAsync(id, dto);
            if (!success) return NotFound(ApiResponse<string>.FailureResponse("Member not found."));
            return Ok(ApiResponse<string>.SuccessResponse(null, "Member details updated successfully."));
        }
        [HttpGet("teams/{franchiseId}")]
        public async Task<IActionResult> GetTeams(int franchiseId)
        {
            var teams = await _hrService.GetTeamsByFranchiseAsync(franchiseId);
            return Ok(ApiResponse<IEnumerable<TeamListDto>>.SuccessResponse(teams, "Teams fetched successfully."));
        }

        [HttpGet("members/{franchiseId}")]
        public async Task<IActionResult> GetMembers(int franchiseId)
        {
            var members = await _hrService.GetMembersByFranchiseAsync(franchiseId);
            return Ok(ApiResponse<IEnumerable<MemberListDto>>.SuccessResponse(members, "Members fetched successfully."));
        }

        [HttpGet("users/{franchiseId}")]
        public async Task<IActionResult> GetUsers(int franchiseId)
        {
            var users = await _hrService.GetUsersByFranchiseAsync(franchiseId);
            return Ok(ApiResponse<IEnumerable<UserListDto>>.SuccessResponse(users, "Users fetched successfully."));
        }
    }
}