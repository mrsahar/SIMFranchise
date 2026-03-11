using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class FranchiseStock
{
    public int Id { get; set; }

    public int FranchiseId { get; set; }

    public string? ProductType { get; set; }

    public int ProductId { get; set; }

    public decimal Quantity { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual Franchise Franchise { get; set; } = null!;
}
