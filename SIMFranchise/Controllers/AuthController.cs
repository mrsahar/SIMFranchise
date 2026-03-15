using Microsoft.AspNetCore.Mvc; 
using SIMFranchise.DTOs.Auth;
using SIMFranchise.Interfaces;
using SIMFranchise.Wrappers; // Ya Services namespace

namespace SIMFranchise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);

            if (result == null)
            {
                return Unauthorized(ApiResponse<string>.FailureResponse("Invalid Email or Password."));
            }

            return Ok(ApiResponse<AuthResponseDto>.SuccessResponse(result, "Login successful."));
        }
    }
}