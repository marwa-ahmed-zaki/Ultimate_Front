using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Ultimate_Front.Views.ViewModel
{
    public class OrderMasterViewModel
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; } 

        public DateOnly OrderDate { get; set; }

        public string AccountNumber { get; set; }
        public string AccountName{ get; set; }
        public string  totalPrice { get; set; }

      
    }
}
