using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class Sale
{
    public int Id { get; set; }

    public int FranchiseId { get; set; }

    public int TeamMemberId { get; set; }

    public string? ProductType { get; set; }

    public int ProductId { get; set; }

    public decimal Quantity { get; set; }

    public decimal TotalAmount { get; set; }

    public string? PaymentMode { get; set; }

    public DateOnly SaleDate { get; set; }

    public decimal UnitCostPrice { get; set; }

    public decimal UnitSalePrice { get; set; }

    public virtual Franchise Franchise { get; set; } = null!;

    public virtual TeamMember TeamMember { get; set; } = null!;
}
