using System;
using System.Collections.Generic;

namespace NEXUS.Models;

public partial class Team
{
    public int PersonId { get; set; }

    public string PersonName { get; set; } = null!;

    public string? PersonImage { get; set; }
}
