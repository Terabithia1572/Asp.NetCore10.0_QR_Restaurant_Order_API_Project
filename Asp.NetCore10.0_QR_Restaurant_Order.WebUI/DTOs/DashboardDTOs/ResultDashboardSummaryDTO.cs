using System;

// Admin paneldeki Dashboard ekranında göstereceğimiz
// özet istatistikleri temsil eden ViewModel/DTO
namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.DashboardDTOs
{
    public class ResultDashboardSummaryDTO
    {
        // Kartlar
        public decimal TodayRevenue { get; set; }        // Bugünkü Ciro
        public int TodayOrderCount { get; set; }         // Bugünkü Sipariş Sayısı
        public decimal MonthlyRevenue { get; set; }      // Bu Ayki Ciro
        public int MonthlyOrderCount { get; set; }       // Bu Ayki Sipariş Sayısı
        public int TotalOrderCount { get; set; }         // Toplam Sipariş
        public int TotalGuestCount { get; set; }         // Toplam Misafir
        public int ActiveTableCount { get; set; }        // Aktif Masa Sayısı

        // Listeler
        public List<ResultTopProductDTO> TopProducts { get; set; } = new();
        public List<ResultRecentOrderDTO> RecentOrders { get; set; } = new();
    }
}
