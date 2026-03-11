using SIMFranchise.DTOs.Product;
using SIMFranchise.Models;

namespace SIMFranchise.Interfaces
{
    public interface ILoadOperatorService
    {
        // 1. Naya Load Operator add karna (e.g. Jazz Load, 2.5% Commission)
        Task<bool> CreateLoadOperatorAsync(LoadOperatorCreateDto dto);

        // 2. Kisi Company ke saare load operators dekhna
        Task<IEnumerable<LoadOperator>> GetOperatorsByCompanyAsync(int companyId);

        // 3. Commission update karna (Agar company commission badal de)
        Task<bool> UpdateCommissionAsync(long id, decimal newCommission);
    }
}