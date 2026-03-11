using System;
using System.Collections.Generic;

namespace SIMFranchise.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    public int? FranchiseId { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual ICollection<DistributionLog> DistributionLogs { get; set; } = new List<DistributionLog>();

    public virtual Role Role { get; set; } = null!;
}
