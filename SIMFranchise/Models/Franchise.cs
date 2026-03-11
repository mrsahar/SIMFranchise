using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class Franchise
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Location { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? Status { get; set; }

    public int CompanyId { get; set; }

    public virtual ICollection<AccountBalance> AccountBalances { get; set; } = new List<AccountBalance>();

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<DistributionLog> DistributionLogs { get; set; } = new List<DistributionLog>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<FranchiseStock> FranchiseStocks { get; set; } = new List<FranchiseStock>();

    public virtual ICollection<LedgerTransaction> LedgerTransactions { get; set; } = new List<LedgerTransaction>();

    public virtual ICollection<MemberStock> MemberStocks { get; set; } = new List<MemberStock>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
