using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class DailyClosing
{
    public long Id { get; set; }

    public int FranchiseId { get; set; }

    public DateOnly ClosingDate { get; set; }

    public decimal SystemCash { get; set; }

    public decimal ActualCash { get; set; }

    public decimal? Shortage { get; set; }

    public int ClosedBy { get; set; }

    public string? Notes { get; set; }

    public virtual TeamMember ClosedByNavigation { get; set; } = null!;

    public virtual Team Franchise { get; set; } = null!;
}
