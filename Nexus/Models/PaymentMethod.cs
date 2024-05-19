using System;
using System.Collections.Generic;

namespace NEXUS.Models;

public partial class PaymentMethod
{
    public int PayId { get; set; }

    public string PayName { get; set; } = null!;
}
