using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.OrderNotificationDTO
{
    public class OrderNotificationDTO
    {
        public int OrderID { get; set; }              // Sipariş ID
        public string TableCode { get; set; } = "";   // Masa kodu / adı (M01, VIP-3 vs.)
        public int TableNumber { get; set; }          // Masa numarası (opsiyonel)
        public string Status { get; set; } = "";      // "Created", "Preparing" vs. (string olarak)
        public string StatusDisplayName { get; set; } = ""; // "Hazırlanıyor", "Servis Edildi"
        public DateTime ChangedAt { get; set; }       // Durum değişim zamanı
        public string ChangedBy { get; set; } = "";   // "Kitchen", "Waiter", "System"
        public string Notes { get; set; } = "";       // Opsiyonel açıklama
    }
}
