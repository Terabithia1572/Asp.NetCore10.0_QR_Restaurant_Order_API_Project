using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.ProductDTO
{
    public class UpdateProductDTO
    {
        public int ProductID { get; set; } // Ürün ID
        public string ProductName { get; set; } // Ürün Adı
        public string ProductDescription { get; set; } // Ürün Açıklaması
        public decimal ProductPrice { get; set; } //Ürün Fiyatı
        public string ProductImageURL { get; set; } // Ürünün Görseli
        public bool ProductStatus { get; set; } // Ürün Durumu (Aktif/Pasif)
        public int CategoryID { get; set; } // Ürünün Kategorisi
    }
}
