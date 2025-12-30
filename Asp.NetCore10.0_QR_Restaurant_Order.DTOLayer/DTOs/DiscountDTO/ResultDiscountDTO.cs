using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.DiscountDTO
{
    public class ResultDiscountDTO
    {
        public int DiscountID { get; set; } //İndirim ID
        public string DiscountTitle { get; set; } //İndirim Başlığı
        public string DiscountDescription { get; set; } //İndirim Açıklaması
        public string DiscountAmount { get; set; } // İndirim Miktarı
        public string DiscountImageURL { get; set; } // İndirim Görsel URL
        public bool DiscountStatus { get; set; } // İndirim Durum
    }
}
