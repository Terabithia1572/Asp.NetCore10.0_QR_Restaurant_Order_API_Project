using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.ReportDTO
{
    public class RevenueSummaryDTO
    {
        public decimal TotalRevenue { get; set; }
        public decimal TodayRevenue { get; set; }
        public decimal ThisWeekRevenue { get; set; }
        public decimal ThisMonthRevenue { get; set; }

        public int TotalOrders { get; set; }
        public int TodayOrders { get; set; }

        public decimal AverageTicketAmount { get; set; }
        public decimal AveragePerTable { get; set; }
        public decimal AveragePerGuest { get; set; }  // GuestCount varsa premium analytics için
    }
}
