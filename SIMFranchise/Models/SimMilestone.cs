using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class SimMilestone
{
    public int Id { get; set; }

    public int MinQty { get; set; }

    public int MaxQty { get; set; }

    public decimal BonusAmount { get; set; }

    public DateOnly EffectiveFrom { get; set; }
}
