using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class TeamMemberKpiTarget
{
    public int Id { get; set; }

    public int TeamMemberId { get; set; }

    public int KpiTypeId { get; set; }

    public decimal TargetValue { get; set; }

    public int? Month { get; set; }

    public int Year { get; set; }

    public decimal CommissionAmount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual KpiType KpiType { get; set; } = null!;

    public virtual TeamMember TeamMember { get; set; } = null!;
}
