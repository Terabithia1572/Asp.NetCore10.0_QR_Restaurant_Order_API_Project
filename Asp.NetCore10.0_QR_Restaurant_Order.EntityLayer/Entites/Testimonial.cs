using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites
{
    public class Testimonial
    {
        public int TestimonialID { get; set; } // Referans ID
        public string TestimonialName { get; set; } // Referans Adı
        public string TestimonialSurname { get; set; } //Referans Soyadı
        public string TestimonialComment { get; set; } // Referans Yorumu
        public string TestimonialImageURL { get; set; } // Referans Resim URL
        public bool TestimonialStatus { get; set; } // Referans Durumu
    }
}
