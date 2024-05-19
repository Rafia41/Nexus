using System;
using System.Collections.Generic;

namespace NEXUS.Models;

public partial class Upgrade
{
    public int UpgradeId { get; set; }

    public int? PackageId { get; set; }

    public virtual ConnectionPackage? Package { get; set; }
}
