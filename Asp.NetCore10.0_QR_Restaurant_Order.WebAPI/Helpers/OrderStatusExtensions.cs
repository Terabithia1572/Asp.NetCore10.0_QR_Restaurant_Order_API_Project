namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Helpers
{
    public static class OrderStatusExtensions
    {
        public static string ToDisplay(int status)
        {
            return status switch
            {
                0 => "Sipariş Oluşturuldu",
                1 => "Sipariş Onaylandı",
                2 => "Sipariş Hazırlanıyor",
                3 => "Sipariş Hazır",
                4 => "Sipariş Servis Edildi",
                5 => "Sipariş İptal Edildi",
                _ => "Bilinmeyen Durum"
            };
        }
    }
}
