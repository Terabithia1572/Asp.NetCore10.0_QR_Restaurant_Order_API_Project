using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.DiscountDTOs
{
    public class CreateDiscountDTO
    {
        public string DiscountTitle { get; set; } //İndirim Başlığı
        public string DiscountDescription { get; set; } //İndirim Açıklaması
        public string DiscountAmount { get; set; } // İndirim Miktarı
        public string DiscountImageURL { get; set; } // İndirim Görsel URL
        public IFormFile? DiscountImageFile { get; set; } // İndirim Görsel Dosyası
        public bool DiscountStatus { get; set; } // İndirim Durum
    }
}
