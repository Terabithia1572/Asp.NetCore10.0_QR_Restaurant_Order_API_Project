using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.DashboardDTO
{
    public class ResultTopProductDTO
    {
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int TotalQuantity { get; set; }   // Kaç adet satılmış
        public decimal TotalAmount { get; set; } // Toplam ciro
    }
}
