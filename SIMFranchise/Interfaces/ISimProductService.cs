using SIMFranchise.DTOs.Product;
using SIMFranchise.Models; 

namespace SIMFranchise.Interfaces
{
    public interface ISimProductService
    {
        // 1. Create Batch with Target
        Task<bool> CreateSimBatchAsync(SimProductCreateDto dto);

        // 2. Fetch all batches for a company (useful for dropdowns)
        Task<IEnumerable<SimProduct>> GetBatchesByCompanyAsync(int companyId);

        // 3. Get Specific Batch by ID
        Task<SimProduct?> GetBatchByIdAsync(long id);

        // 4. Update Batch Details
        Task<bool> UpdateSimBatchAsync(long id, SimProductCreateDto dto);
    }
}