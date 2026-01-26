using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.KitchenOrderDetailDTO
{
    public class KitchenOrderDetailDTO
    {
        public int OrderID { get; set; }
        public int TableNumber { get; set; }
        public int Status { get; set; }
        public string StatusText { get; set; }
        public decimal Total { get; set; }
        public List<KitchenOrderDetailItemDto> Items { get; set; } = new();
    }
}
