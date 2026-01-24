using System.Collections.Generic;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.DashboardDTO
{
    public class ResultDashboardSummaryDTO
    {
        // Bugün toplam ciro (TL)
        public decimal TodayRevenue { get; set; }

        // Bugünkü sipariş sayısı
        public int TodayOrderCount { get; set; }

        // İçinde bulunulan ayın toplam cirosu
        public decimal MonthlyRevenue { get; set; }

        // İçinde bulunulan ayın sipariş sayısı
        public int MonthlyOrderCount { get; set; }

        // Sistemdeki toplam sipariş sayısı
        public int TotalOrderCount { get; set; }

        // Tüm siparişlerdeki toplam misafir sayısı
        public int TotalGuestCount { get; set; }

        // Şu an aktif olan masa sayısı
        public int ActiveTableCount { get; set; }

        // Dashboard sağ tarafta gösterdiğimiz en çok satan ürünler
        public List<ResultTopProductDTO> TopProducts { get; set; } = new();

        // Son siparişler tablosu
        public List<ResultRecentOrderDTO> RecentOrders { get; set; } = new();
    }
}
