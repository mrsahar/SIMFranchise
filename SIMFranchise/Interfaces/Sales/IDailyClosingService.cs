using SIMFranchise.DTOs.Closing;
using SIMFranchise.Models;

namespace SIMFranchise.Interfaces.Sales
{
    public interface IDailyClosingService
    {
        Task<bool> PerformDailyClosingAsync(DailyClosingCreateDto dto);
        Task<IEnumerable<DailyClosing>> GetClosingHistoryAsync(int franchiseId);
    }
}
