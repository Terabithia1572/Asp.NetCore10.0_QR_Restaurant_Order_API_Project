using Asp.NetCore10._0_QR_Restaurant_Order.UI.Controllers;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models
{
    public class ReservationViewModel
    {
        public int Id { get; set; }

        // Temel
        public string CustomerName { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public int GuestCount { get; set; }

        // Tarih / Masa
        public DateTime ReservationDate { get; set; }
        public int? TableId { get; set; }

        // Durum / Kaynak / VIP
        public ReservationStatus Status { get; set; }
        public ReservationSource Source { get; set; }
        public bool IsVip { get; set; }

        // Not
        public string Notes { get; set; } = "";
    }
}
