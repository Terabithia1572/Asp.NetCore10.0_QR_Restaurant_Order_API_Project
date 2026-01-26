namespace Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.ReportDTO
{
    public class ProductPerformanceDTO
    {
        public string ProductName { get; set; } = string.Empty;
        public int TotalQuantity { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal PercentageOfTotalRevenue { get; set; }
    }
}
