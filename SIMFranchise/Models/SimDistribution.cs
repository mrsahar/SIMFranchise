using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class SimDistribution
{
    public long Id { get; set; }

    public long SimProductId { get; set; }

    public int TeamMemberId { get; set; }

    public decimal AllocatedQuantity { get; set; }

    public DateTime? DistributedDate { get; set; }

    public string? Notes { get; set; }

    public virtual SimProduct SimProduct { get; set; } = null!;

    public virtual TeamMember TeamMember { get; set; } = null!;
}
