using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.FeatureDTOs
{
    public class CreateFeatureDTO
    {
        public string FeatureTitle { get; set; } // Öne Çıkanlar Başlık
        public string FeatureDescription { get; set; } // Öne Çıkanlar Açıklama
        public bool FeatureStatus { get; set; } // Öne Çıkanlar Durum
    }
}
