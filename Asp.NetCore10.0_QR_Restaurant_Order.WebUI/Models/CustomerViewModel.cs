namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        // Temel Bilgiler
        public string FullName { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";

        // CRM / Loyalty Alanları
        public int TotalOrders { get; set; }
        public decimal TotalSpend { get; set; }
        public decimal AverageTicket { get; set; }   // TotalSpend / TotalOrders
        public DateTime? LastOrderDate { get; set; }

        public List<string> Tags { get; set; } = new();
        public string Notes { get; set; } = "";
    }
}
