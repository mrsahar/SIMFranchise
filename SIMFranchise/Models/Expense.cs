using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class Expense
{
    public int Id { get; set; }

    public int FranchiseId { get; set; }

    public string Category { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? PaidFrom { get; set; }

    public DateOnly ExpenseDate { get; set; }

    public string? Notes { get; set; }

    public int RecordedBy { get; set; }

    public string? Description { get; set; }

    public virtual Franchise Franchise { get; set; } = null!;

    public virtual TeamMember RecordedByNavigation { get; set; } = null!;
}
