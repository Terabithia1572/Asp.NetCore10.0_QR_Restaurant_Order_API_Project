using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.DashboardDTO
{
    public class ResultRecentOrderDTO
    {
        public int OrderID { get; set; }
        public string TableName { get; set; }
        public string CustomerName { get; set; } // İstersen null bırak
        public DateTime CreatedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Profit { get; set; }      // Şimdilik fake, sonra maliyet eklersen net hesaplanır
        public string StatusText { get; set; }   // Hazırlanıyor / Serviste / Tamamlandı
    }
}
