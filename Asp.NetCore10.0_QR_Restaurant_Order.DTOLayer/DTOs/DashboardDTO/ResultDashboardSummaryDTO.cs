using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.DashboardDTO
{
    public class ResultDashboardSummaryDTO
    {
        public decimal TodayRevenue { get; set; }          // Bugünkü ciro
        public int TodayOrderCount { get; set; }           // Bugünkü sipariş adedi
        public decimal MonthlyRevenue { get; set; }        // Bu ayki ciro
        public int MonthlyOrderCount { get; set; }         // Bu ayki sipariş sayısı
        public int TotalOrderCount { get; set; }           // Tüm zamanlar sipariş
        public int TotalGuestCount { get; set; }           // Toplam misafir (Orders.GuestCount veya Bookings)
        public int ActiveTableCount { get; set; }          // Şu an dolu/aktif masa sayısı

        public List<ResultTopProductDTO> TopProducts { get; set; }
        public List<ResultRecentOrderDTO> RecentOrders { get; set; }
    }
}
