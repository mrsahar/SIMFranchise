using SIMFranchise.DTOs.Dashboard;

namespace SIMFranchise.Interfaces.Dashboard
{
    public interface IDashboardService
    {
        Task<FranchiseDashboardDto> GetFranchiseDashboardAsync(int franchiseId);
    }
}
