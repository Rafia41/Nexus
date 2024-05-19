using System;
using System.Collections.Generic;

namespace NEXUS.Models;

public partial class Contact
{
    public int ContId { get; set; }

    public string ContName { get; set; } = null!;

    public string ContEmail { get; set; } = null!;

    public string ContPhone { get; set; } = null!;

    public string ContAddress { get; set; } = null!;

    public string? ContMessage { get; set; }
}
