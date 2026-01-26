namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models
{
    public class CouponViewModel
    {
        public int Id { get; set; }
        public string Code { get; set; } = "";
        public decimal Discount { get; set; }
        public string Type { get; set; } = "";
        public int UsageLimit { get; set; }
        public int Used { get; set; }
        public bool Status { get; set; }
    }
}
