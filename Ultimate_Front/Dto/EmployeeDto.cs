﻿namespace Ultimate_Front.Dto
{
    public class EmployeeDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
}
