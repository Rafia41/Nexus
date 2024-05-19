using System;
using System.Collections.Generic;

namespace NEXUS.Models;

public partial class SecurityDeposit
{
    public int SecurityId { get; set; }

    public string SecurityName { get; set; } = null!;
}
