using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.OrderDTO
{
    public class GetOrderByIDDTO
    {
        public int OrderID { get; set; }
        public int TableID { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalPrice { get; set; }
        public int? GuestCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OrderStatus { get; set; } // enum'a da çevirebilirsin
        public bool PaymentStatus { get; set; }

       
    }
}
