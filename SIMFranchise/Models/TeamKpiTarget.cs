using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class TeamKpiTarget
{
    public int Id { get; set; }

    public int TeamId { get; set; }

    public int KpiTypeId { get; set; }

    public decimal TargetValue { get; set; }

    public int? Month { get; set; }

    public int Year { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual KpiType KpiType { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
