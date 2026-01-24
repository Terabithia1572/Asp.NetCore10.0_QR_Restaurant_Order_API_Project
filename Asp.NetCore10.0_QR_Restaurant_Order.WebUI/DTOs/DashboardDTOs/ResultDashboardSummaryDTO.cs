using System;

// Admin paneldeki Dashboard ekranında göstereceğimiz
// özet istatistikleri temsil eden ViewModel/DTO
namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.DashboardDTOs
{
    public class ResultDashboardSummaryDTO
    {
        // Bugüne ait toplam sipariş sayısı
        public int TodayTotalOrderCount { get; set; }

        // Bugüne ait toplam ciro (₺)
        public decimal TodayTotalRevenue { get; set; }

        // Şu anda aktif olan (açık) masa sayısı
        public int ActiveTableCount { get; set; }

        // Ortalama servis süresi (dakika)
        public int AverageServiceTimeMinute { get; set; }

        // Bugün yapılan toplam QR tarama sayısı
        public int TodayQrScanCount { get; set; }

        // Bugün restorana ilk defa gelen yeni müşteri sayısı
        public int TodayNewCustomerCount { get; set; }
    }
}
