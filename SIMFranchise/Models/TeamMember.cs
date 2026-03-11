using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class TeamMember
{
    public int Id { get; set; }

    public int TeamId { get; set; }

    public string Name { get; set; } = null!;

    public decimal BaseSalary { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<DailyClosing> DailyClosings { get; set; } = new List<DailyClosing>();

    public virtual ICollection<DistributionLog> DistributionLogs { get; set; } = new List<DistributionLog>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<MemberStock> MemberStocks { get; set; } = new List<MemberStock>();

    public virtual ICollection<Salary> Salaries { get; set; } = new List<Salary>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<SimDistribution> SimDistributions { get; set; } = new List<SimDistribution>();

    public virtual Team Team { get; set; } = null!;

    public virtual ICollection<TeamMemberKpiTarget> TeamMemberKpiTargets { get; set; } = new List<TeamMemberKpiTarget>();
}
