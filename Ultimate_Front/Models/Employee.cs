using System;
using System.Collections.Generic;

namespace Ultimate_Front.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? EmployeeName { get; set; }

     public virtual ICollection<OrderMaster> OrderMasters { get; set; } = new List<OrderMaster>();

}
