using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class AccountBalance
{
    public int FranchiseId { get; set; }

    public string AccountType { get; set; } = null!;

    public decimal CurrentBalance { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual Franchise Franchise { get; set; } = null!;
}
