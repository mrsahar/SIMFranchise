using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class Team
{
    public int Id { get; set; }

    public int FranchiseId { get; set; }

    public string Name { get; set; } = null!;

    public string? Responsibility { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<DailyClosing> DailyClosings { get; set; } = new List<DailyClosing>();

    public virtual Franchise Franchise { get; set; } = null!;

    public virtual ICollection<TeamKpiTarget> TeamKpiTargets { get; set; } = new List<TeamKpiTarget>();

    public virtual ICollection<TeamMember> TeamMembers { get; set; } = new List<TeamMember>();
}
