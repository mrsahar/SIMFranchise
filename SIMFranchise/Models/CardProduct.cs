using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class CardProduct
{
    public long Id { get; set; }

    public int CompanyId { get; set; }

    public string CardName { get; set; } = null!;

    public int CardValue { get; set; }

    public decimal CostPrice { get; set; }

    public decimal SalePrice { get; set; }

    public bool? IsActive { get; set; }

    public virtual Company Company { get; set; } = null!;
}
