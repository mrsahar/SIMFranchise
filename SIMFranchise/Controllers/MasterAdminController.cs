using Microsoft.AspNetCore.Mvc;
using SIMFranchise.DTOs.Admin;
using SIMFranchise.Interfaces;
using SIMFranchise.Wrappers;

[Route("api/[controller]")]
[ApiController]
public class MasterAdminController : ControllerBase
{
    private readonly IMasterAdminService _adminService;
    public MasterAdminController(IMasterAdminService adminService) { _adminService = adminService; }

    [HttpPost("manage-role")]
    public async Task<IActionResult> ManageRole(int? id, MasterRoleDto dto) => Ok(await _adminService.UpsertRoleAsync(id, dto));

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser(MasterUserDto dto) => Ok(await _adminService.CreateUserAsync(dto));

    [HttpPost("setup-team")]
    public async Task<IActionResult> CreateTeam(MasterTeamDto dto) => Ok(await _adminService.CreateTeamAsync(dto));

    [HttpPost("add-member")]
    public async Task<IActionResult> AddMember(int teamId, string name, decimal salary) => Ok(await _adminService.QuickAddMemberAsync(teamId, name, salary));

    [HttpPatch("toggle-user/{userId}")]
    public async Task<IActionResult> ToggleUser(int userId, bool status) => Ok(await _adminService.ToggleUserStatusAsync(userId, status));

    // Member ko delete karne ke liye
    [HttpDelete("remove-member/{memberId}")]
    public async Task<IActionResult> RemoveMember(int memberId)
    {
        var success = await _adminService.RemoveMemberAsync(memberId);
        if (!success) return NotFound(ApiResponse<string>.FailureResponse("Member not found or could not be removed."));

        return Ok(ApiResponse<string>.SuccessResponse(  "Team member has been removed successfully."));
    }

    // Member ki team badalne ke liye
    [HttpPatch("change-member-team")]
    public async Task<IActionResult> ChangeTeam(int memberId, int newTeamId)
    {
        var success = await _adminService.ChangeMemberTeamAsync(memberId, newTeamId);
        if (!success) return BadRequest(ApiResponse<string>.FailureResponse("Invalid Member ID or Team ID."));

        return Ok(ApiResponse<string>.SuccessResponse( "Member team has been updated successfully."));
    }
}