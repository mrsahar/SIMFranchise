using SIMFranchise.DTOs.Franchise;
using SIMFranchise.DTOs.Franchise.SIMFranchise.DTOs.Franchise;

namespace SIMFranchise.Interfaces
{
    public interface IFranchiseService
    {
        Task<List<FranchiseResponseDto>> GetAllFranchisesAsync();
        Task<FranchiseResponseDto?> GetFranchiseByIdAsync(int id);
        Task<FranchiseResponseDto> CreateFranchiseAsync(FranchiseCreateDto dto);
        Task<bool> UpdateFranchiseAsync(int id, FranchiseUpdateDto dto);
    }
}