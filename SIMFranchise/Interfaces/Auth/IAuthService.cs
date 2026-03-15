using SIMFranchise.DTOs.Auth;

namespace SIMFranchise.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }
}