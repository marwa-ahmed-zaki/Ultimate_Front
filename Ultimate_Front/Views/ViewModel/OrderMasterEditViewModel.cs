namespace Ultimate_Front.Views.ViewModel
{
    public class OrderMasterEditViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } = null!;

        public DateOnly OrderDate { get; set; }

        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string totalPrice { get; set; }
    }
}
