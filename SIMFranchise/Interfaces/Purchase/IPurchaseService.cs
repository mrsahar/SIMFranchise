using SIMFranchise.DTOs.Purchase;

namespace SIMFranchise.Interfaces.Purchase
{
    public interface IPurchaseService
    {
        Task<bool> ProcessPurchaseAsync(PurchaseCreateDto dto);
        Task<IEnumerable<PurchaseHistoryDto>> GetPurchaseHistoryAsync(int franchiseId, DateTime fromDate, DateTime toDate);
    }
}