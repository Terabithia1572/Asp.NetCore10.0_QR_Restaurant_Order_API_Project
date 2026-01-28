namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.ProductDTOs
{
    public class CreateProductDTO
    {
        public string ProductName { get; set; } // Ürün Adı
        public string ProductDescription { get; set; } // Ürün Açıklaması
        public decimal ProductPrice { get; set; } //Ürün Fiyatı

        // Ürünün görsel yolu (API/DB’ye bu gider)
        public string? ProductImageURL { get; set; }

        // Dosya upload (sadece UI tarafında kullanılır)
        public IFormFile? ProductImageFile { get; set; }

        public bool ProductStatus { get; set; } // Ürün Durumu (Aktif/Pasif)
        public int CategoryID { get; set; } // Ürünün Kategorisi

    }
}
