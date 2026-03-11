using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class KpiType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TeamKpiTarget> TeamKpiTargets { get; set; } = new List<TeamKpiTarget>();

    public virtual ICollection<TeamMemberKpiTarget> TeamMemberKpiTargets { get; set; } = new List<TeamMemberKpiTarget>();
}
