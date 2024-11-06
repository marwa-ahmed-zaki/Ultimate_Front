﻿using System;
using System.Collections.Generic;

namespace Ultimate_Front.Models;

public partial class OrderDetail
{
    public int OrderDetailsId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal? TotalPrice { get; set; }

    public virtual OrderMaster Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
