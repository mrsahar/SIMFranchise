namespace SIMFranchise.DTOs.LedgerTransaction
{
         public class LedgerCreateDto
        {
            public int FranchiseId { get; set; }  
            public string AccountType { get; set; } = null!;  
            public decimal Amount { get; set; } 
            public string? Note { get; set; } 
        } 
}
