using System;
using System.Collections.Generic;
using System.Text;

namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.ReportDTO
{
    public class ReportsDTO
    {
        public RevenueSummaryDTO RevenueSummary { get; set; } = new RevenueSummaryDTO();
        public List<RevenueChartPointDTO> RevenueLast30Days { get; set; } = new();
        public List<ProductPerformanceDTO> TopProducts { get; set; } = new();
        public List<PeakHourDTO> PeakHours { get; set; } = new();
    }
}
