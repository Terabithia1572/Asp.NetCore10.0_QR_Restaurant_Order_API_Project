namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models
{
    public class TableStatusViewModel
    {
        public int TableID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string Status { get; set; }  // Boş / Dolu / Hesap / Rezerve
    }
}
