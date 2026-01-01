namespace Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites
{
    public class Category
    {
        public int CategoryID { get; set; } // Kategori ID
        public string CategoryName { get; set; } // Kategori Adı
        public bool CategoryStatus { get; set; } // Kategori Durumu (Aktif/Pasif)
        public List<Product> Products { get; set; } // Kategoriye Ait Ürünler
    }
}
