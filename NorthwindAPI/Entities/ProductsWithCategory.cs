using System;
using System.Collections.Generic;

namespace NorthwindAPI.Entities;

public partial class ProductsWithCategory
{
    public string ProductName { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public short Quantity { get; set; }

    public DateTime? OrderDate { get; set; }
}
