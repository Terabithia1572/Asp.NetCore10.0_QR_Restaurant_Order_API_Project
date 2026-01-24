using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.OrderDetailDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.OrderDTO
{
    public class CreateOrderDTO
    {
        // Hangi masadan sipariş geldi?
        public int TableID { get; set; }

        // İstersen QR tarafta boş bırakabilirsin, UI'dan doldurursun
        public string? CustomerName { get; set; }

        // Toplam tutar (UI hesaplayıp gönderebilir veya backend’de hesaplayabilirsin)
        public decimal TotalPrice { get; set; }

        // Kaç kişi oturuyor (opsiyonel ama güzel bilgi)
        public int GuestCount { get; set; }

        // OrderStatus -> Order entity’de int olduğu için burada da int
        // 0: Oluşturuldu, 1: Onaylandı, 2: Hazırlanıyor, 3: Hazır, 4: Servis Edildi, 5: İptal, vs.
        public int OrderStatus { get; set; }

        // PaymentStatus -> Ödeme durumu (örneğin 0: Ödenmedi, 1: Ödendi)
        public bool PaymentStatus { get; set; }

        // Sipariş içindeki ürünler
        public List<CreateOrderDetailDTO>? OrderDetails { get; set; }
        public List<CreateOrderItemDTO> Items { get; set; }
    }
}
