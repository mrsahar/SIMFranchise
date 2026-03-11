using SIMFranchise.DTOs.Reports;
using SIMFranchise.DTOs.Sales;

namespace SIMFranchise.Interfaces
{
    public interface IReportService
    {
        // Franchise ki Grand Summary (Today, Yesterday, Weekly, Monthly)
        Task<FranchiseGrandReportDto> GetFranchiseGrandReportAsync(int franchiseId, string filterType);

        // (Optional) Aglay step mein Member-wise report bhi yahan add karenge
        Task<List<MemberSaleReportDto>> GetMemberSaleReportAsync(int franchiseId, string filterType);
    }
}