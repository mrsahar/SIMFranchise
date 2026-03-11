using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class Purchase
{
    public long Id { get; set; }

    public int FranchiseId { get; set; }

    public int CompanyId { get; set; }

    public string? ProductType { get; set; }

    public long ProductId { get; set; }

    public decimal Quantity { get; set; }

    public decimal TotalAmount { get; set; }

    public DateOnly PurchaseDate { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Franchise Franchise { get; set; } = null!;
}
