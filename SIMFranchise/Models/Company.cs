using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Contact { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<CardProduct> CardProducts { get; set; } = new List<CardProduct>();

    public virtual ICollection<Franchise> Franchises { get; set; } = new List<Franchise>();

    public virtual ICollection<LoadOperator> LoadOperators { get; set; } = new List<LoadOperator>();

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();

    public virtual ICollection<SimProduct> SimProducts { get; set; } = new List<SimProduct>();
}
