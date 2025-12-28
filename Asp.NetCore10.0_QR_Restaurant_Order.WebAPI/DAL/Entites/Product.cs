namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.DAL.Entites
{
    public class Product
    {
        public int ProductID { get; set; } // Ürün ID
        public string ProductName { get; set; } // Ürün Adı
        public string ProductDescription { get; set; } // Ürün Açıklaması
        public decimal ProductPrice { get; set; } //Ürün Fiyatı
        public string ProductImageURL { get; set; } // Ürünün Görseli
        public bool ProductStatus { get; set; } // Ürün Durumu (Aktif/Pasif)
    }
}
