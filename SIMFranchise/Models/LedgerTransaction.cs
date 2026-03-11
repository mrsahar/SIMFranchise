using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class LedgerTransaction
{
    public long Id { get; set; }

    public int FranchiseId { get; set; }

    public string AccountType { get; set; } = null!;

    public string Direction { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? Source { get; set; }

    public long? SourceId { get; set; }

    public DateOnly? TxnDate { get; set; }

    public string? Note { get; set; }

    public virtual Franchise Franchise { get; set; } = null!;
}
