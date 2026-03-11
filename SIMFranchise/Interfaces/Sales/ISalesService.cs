using SIMFranchise.DTOs.Sales;
using SIMFranchise.Models;

namespace SIMFranchise.Interfaces
{
    public interface ISalesService
    {
        // 1. Sale record karna (Stock minus + Ledger plus + Sale Entry)
        Task<bool> ProcessSaleAsync(SaleCreateDto dto);

        // 2. Kisi member ki aaj ki total sales dekhna
        Task<IEnumerable<Sale>> GetMemberDailySalesAsync(int memberId, DateTime date);

        // 3. Franchise ki overall sales report
        Task<IEnumerable<Sale>> GetFranchiseSalesReportAsync(int franchiseId, DateTime fromDate, DateTime toDate);
    }
}