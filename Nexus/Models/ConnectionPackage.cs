using System;
using System.Collections.Generic;

namespace NEXUS.Models;

public partial class ConnectionPackage
{
    public int PackageId { get; set; }

    public string PackageName { get; set; } = null!;

    public virtual ICollection<PackagePlan> PackagePlans { get; } = new List<PackagePlan>();
}
