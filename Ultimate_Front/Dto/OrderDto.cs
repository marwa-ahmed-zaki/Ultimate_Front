namespace Ultimate_Front.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string OrderDate { get; set; } 
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string TotalPrice { get; set; } 
    }
}
