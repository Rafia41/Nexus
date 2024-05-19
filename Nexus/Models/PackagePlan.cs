using System;
using System.Collections.Generic;

namespace NEXUS.Models;

public partial class PackagePlan
{
    public int PackId { get; set; }

    public string PackName { get; set; } = null!;

    public string PackDesc { get; set; } = null!;

    public long PackPrice { get; set; }

    public string PackValidity { get; set; } = null!;

    public int? PackageId { get; set; }

    public virtual ConnectionPackage? Package { get; set; }
}
