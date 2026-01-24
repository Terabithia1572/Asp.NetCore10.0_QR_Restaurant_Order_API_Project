using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites
{
    public class Order
    {
        public int OrderID { get; set; }
        public int TableID { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalPrice { get; set; }
        public int? GuestCount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OrderStatus { get; set; } // enum'a da çevirebilirsin
        public bool PaymentStatus { get; set; }

        public Table Table { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
