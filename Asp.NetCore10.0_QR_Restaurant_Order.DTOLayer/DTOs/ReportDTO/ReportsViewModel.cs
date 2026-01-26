namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.ReportDTO
{
    public class ReportsViewModel
    {
        // Üst KPI kartları
        public RevenueSummaryViewModel RevenueSummary { get; set; } = new RevenueSummaryViewModel();

        // Grafikler
        public List<RevenueChartPointDTO> RevenueLast30Days { get; set; } = new();
        public List<ProductPerformanceDTO> TopProducts { get; set; } = new();
        public List<PeakHourDTO> PeakHours { get; set; } = new();

        // İleride: Table metrics, Customer insights vs. buraya eklersin
    }
}
