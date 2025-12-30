using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.FooterDTO
{
    public class UpdateFooterDTO
    {
        public int FooterID { get; set; } // Footer ID'si
        public string FooterTitle { get; set; } // Footer Başlığı
        public string FooterDescription { get; set; } // Footer Açıklaması
        public string FooterFacebook { get; set; } // Footer Facebook Linki
        public string FooterInstagram { get; set; } // Footer Instagram Linki
        public string FooterTwitter { get; set; } // Footer Twitter Linki
        public string FooterLinkedIn { get; set; } //Footer Linkedin Linki
        public string FooterPinterest { get; set; } // Footer Pinterest Linki
        public bool FooterStatus { get; set; } // Footer Durumu
    }
}
