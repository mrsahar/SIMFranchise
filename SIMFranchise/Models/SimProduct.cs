using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class SimProduct
{
    public long Id { get; set; }

    public int CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public decimal CostPrice { get; set; }

    public decimal SalePrice { get; set; }

    public bool? IsActive { get; set; }

    public decimal ActivationTarget { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<SimDistribution> SimDistributions { get; set; } = new List<SimDistribution>();
}
