namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models
{
    public class ExtraItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Category { get; set; } = "";
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
