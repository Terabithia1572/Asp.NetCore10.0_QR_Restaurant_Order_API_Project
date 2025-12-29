namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.DAL.Entites
{
    public class About
    {
        public int AboutID { get; set; } //Hakkımda ID
        public string AboutImageURL { get; set; } // Hakkımda Görsel URL
        public string AboutTitle { get; set; } // Hakkımda Başlık
        public string AboutDescription { get; set; } // Hakkımda Açıklama
        public bool AboutStatus { get; set; } // Hakkımda Durum
    }
}
