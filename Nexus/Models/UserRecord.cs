using System;
using System.Collections.Generic;

namespace NEXUS.Models;

public partial class UserRecord
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public int? UserRoleId { get; set; }

    public virtual Role? UserRole { get; set; }
}
