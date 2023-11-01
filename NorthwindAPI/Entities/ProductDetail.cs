using System;
using System.Collections.Generic;

namespace NorthwindAPI.Entities;

public partial class ProductDetail
{
    public string ProductName { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public int ProductId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }

    public string? QuantityPerUnit { get; set; }

    public decimal? UnitPrice { get; set; }

    public short? UnitsInStock { get; set; }

    public short? UnitsOnOrder { get; set; }

    public short? ReorderLevel { get; set; }

    public bool Discontinued { get; set; }
}
