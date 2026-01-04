using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.AboutDTOs
{
    public class GetAboutByIDDTO
    {
        public int AboutID { get; set; } //Hakkımda ID

        // API’ye gidecek görsel yolu (örn: /aboutimages/xyz.webp)
        public string? AboutImageURL { get; set; }

        // UI tarafında formdan gelecek dosya
        public IFormFile? AboutImageFile { get; set; }
        public string AboutTitle { get; set; } // Hakkımda Başlık
        public string AboutDescription { get; set; } // Hakkımda Açıklama
        public bool AboutStatus { get; set; } // Hakkımda Durum
    }
}
