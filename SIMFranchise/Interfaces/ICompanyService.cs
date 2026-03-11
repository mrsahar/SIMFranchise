using SIMFranchise.DTOs.Company;

namespace SIMFranchise.Interfaces
{
    public interface ICompanyService
    {
        Task<List<CompanyResponseDto>> GetAllCompaniesAsync();
        Task<CompanyResponseDto?> GetCompanyByIdAsync(int id);
        Task<CompanyResponseDto> CreateCompanyAsync(CompanyCreateDto dto);
        Task<bool> UpdateCompanyAsync(int id, CompanyUpdateDto dto);
    }
}
