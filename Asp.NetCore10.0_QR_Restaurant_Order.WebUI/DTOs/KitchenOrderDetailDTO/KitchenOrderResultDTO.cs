using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.OrderDTO
{
    public class KitchenOrderResultDTO
    {
        public int OrderID { get; set; }
        public int TableID { get; set; }
        public string? CustomerName { get; set; }
        public decimal TotalPrice { get; set; }
        public int? GuestCount { get; set; }
        public int OrderStatus { get; set; }
        public DateTime CreatedDate { get; set; }

        // Mutfak ekranı için insan okunur durum
        public string StatusText { get; set; } = "";
    }
}
