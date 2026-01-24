using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.OrderDTO
{
    public class CreateOrderDTO
    {
        public int TableID { get; set; }
        public int? GuestCount { get; set; }
        public List<CreateOrderItemDTO> Items { get; set; }
    }
}
