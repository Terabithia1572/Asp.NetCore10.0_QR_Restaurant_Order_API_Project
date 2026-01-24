namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.DashboardDTOs
{
    public class ResultRecentOrderDTO
    {
        public int OrderID { get; set; }
        public string TableName { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Profit { get; set; }
        public string StatusText { get; set; }
    }
}
