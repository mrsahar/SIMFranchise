using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class DistributionLog
{
    public long Id { get; set; }

    public int FranchiseId { get; set; }

    public int TeamMemberId { get; set; }

    public string ProductType { get; set; } = null!;

    public int ProductId { get; set; }

    public decimal Quantity { get; set; }

    public string EntryType { get; set; } = null!;

    public DateTime? LogDate { get; set; }

    public int IssuedBy { get; set; }

    public virtual Franchise Franchise { get; set; } = null!;

    public virtual User IssuedByNavigation { get; set; } = null!;

    public virtual TeamMember TeamMember { get; set; } = null!;
}
