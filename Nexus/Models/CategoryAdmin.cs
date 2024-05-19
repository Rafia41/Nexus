using System;
using System.Collections.Generic;

namespace NEXUS.Models;

public partial class CategoryAdmin
{
    public int CatgId { get; set; }

    public string CatgName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
