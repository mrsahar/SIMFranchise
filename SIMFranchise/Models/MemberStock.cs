using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class MemberStock
{
    public int Id { get; set; }

    public int FranchiseId { get; set; }

    public int TeamMemberId { get; set; }

    public string ProductType { get; set; } = null!;

    public int ProductId { get; set; }

    public decimal? CurrentQuantity { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual Franchise Franchise { get; set; } = null!;

    public virtual TeamMember TeamMember { get; set; } = null!;
}
