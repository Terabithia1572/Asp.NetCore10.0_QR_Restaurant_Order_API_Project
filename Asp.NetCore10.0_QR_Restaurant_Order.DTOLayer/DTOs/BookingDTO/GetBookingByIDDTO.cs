using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.BookingDTO
{
    public class GetBookingByIDDTO
    {
        public int BookingID { get; set; } //Rezervasyon ID
        public string BookingName { get; set; } // Rezervasyon Adı
        public string BookingPhone { get; set; } //Rezervasyon Telefon Numarası
        public string BookingMail { get; set; } // Rezervasyon Maili
        public int BookingPersonCount { get; set; } // Rezervasyon Kişi Sayısı
        public DateTime BookingDate { get; set; } // Rezervasyon Tarihi
        public bool BookingStatus { get; set; } //Rezervasyon Durumu
    }
}
