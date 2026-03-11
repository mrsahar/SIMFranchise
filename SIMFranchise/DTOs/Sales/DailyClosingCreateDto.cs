namespace SIMFranchise.DTOs.Closing
{
    public class DailyClosingCreateDto
    {
        public int FranchiseId { get; set; }
        public decimal ActualCash { get; set; }
        public int ClosedByUserId { get; set; }
        public string? Notes { get; set; }
    }
}