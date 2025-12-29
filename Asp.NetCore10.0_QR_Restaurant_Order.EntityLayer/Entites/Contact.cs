using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites
{
    public class Contact
    {
        public int ContactID { get; set; } // İletişim ID
        public string ContactLocation { get; set; } //İletişim Haritası
        public string ContactPhone { get; set; } // İletişim Numarası
        public string ContactMail { get; set; } //İletişim Maili
        public bool ContactStatus { get; set; } // İletişim Durumu
    }
}
