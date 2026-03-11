using SIMFranchise.DTOs.Stock;
using SIMFranchise.Models;

namespace SIMFranchise.Interfaces
{
    public interface IDistributionService
    {
        // 1. Franchise se Member ko stock issue karna
        Task<bool> IssueStockToMemberAsync(StockDistributionDto dto);

        // 2. Member se bacha hua stock wapas lena (Returns)
        Task<bool> ReturnStockToFranchiseAsync(StockDistributionDto dto);

        // 3. Kisi specific member ka current stock check karna
        Task<IEnumerable<MemberStock>> GetMemberCurrentStockAsync(int memberId);

        // 4. Distribution ki history (Logs) dekhna
        Task<IEnumerable<DistributionLog>> GetDistributionLogsAsync(int franchiseId);
    }
}