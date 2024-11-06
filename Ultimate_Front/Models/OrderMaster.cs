using System;
using System.Collections.Generic;

namespace Ultimate_Front.Models;

public partial class OrderMaster
{
    public int OrderId { get; set; }

    public string OrderNo { get; set; } = null!;

    public DateOnly OrderDate { get; set; }

    public int CustomerId { get; set; }

    public string AccountCode { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsComplete { get; set; }

    public int? EmployeeId { get; set; }

    public DateOnly? CompletedDate { get; set; }

    public virtual Account AccountCodeNavigation { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
