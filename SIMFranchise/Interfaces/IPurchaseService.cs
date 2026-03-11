using SIMFranchise.DTOs.Purchase;

namespace SIMFranchise.Interfaces
{
    public interface IPurchaseService
    {
        Task<bool> ProcessPurchaseAsync(PurchaseCreateDto dto);
    }
}