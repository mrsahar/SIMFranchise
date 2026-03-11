using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class LoadOperator
{
    public long Id { get; set; }

    public int CompanyId { get; set; }

    public string OperatorName { get; set; } = null!;

    public decimal CommissionPercent { get; set; }

    public bool? IsActive { get; set; }

    public virtual Company Company { get; set; } = null!;
}
