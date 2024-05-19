using System;
using System.Collections.Generic;

namespace NEXUS.Models;

public partial class Product
{
    public int ProdId { get; set; }

    public string ProdName { get; set; } = null!;

    public string ProdDesc { get; set; } = null!;

    public string? ProdImage { get; set; }

    public long ProdPrice { get; set; }

    public int? ProdCatgId { get; set; }

    public virtual CategoryAdmin? ProdCatg { get; set; }
}
