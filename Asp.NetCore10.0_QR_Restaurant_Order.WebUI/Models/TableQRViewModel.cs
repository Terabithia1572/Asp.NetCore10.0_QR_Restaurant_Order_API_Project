namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models
{
    public class TableQRViewModel
    {
        public int TableID { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public string QRUrl { get; set; }   // QR'ın yönleneceği link
    }
}
