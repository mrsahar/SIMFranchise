using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class Salary
{
    public int Id { get; set; }

    public int TeamMemberId { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }

    public decimal BaseSalary { get; set; }

    public decimal? Commission { get; set; }

    public decimal? Bonus { get; set; }

    public decimal TotalPaid { get; set; }

    public DateOnly PaidDate { get; set; }

    public virtual TeamMember TeamMember { get; set; } = null!;
}
