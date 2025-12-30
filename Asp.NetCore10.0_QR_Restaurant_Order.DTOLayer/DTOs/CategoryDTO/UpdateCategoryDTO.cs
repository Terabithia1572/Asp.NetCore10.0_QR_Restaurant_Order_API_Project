using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.CategoryDTO
{
    public class UpdateCategoryDTO
    {
        public int CategoryID { get; set; } // Kategori ID
        public string CategoryName { get; set; } // Kategori Adı
        public bool CategoryStatus { get; set; } // Kategori Durumu (Aktif/Pasif)
    }
}
