using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.OrderDTO
{
    public class CreateOrderItemDTO
    {
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
