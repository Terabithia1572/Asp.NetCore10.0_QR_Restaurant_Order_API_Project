using Asp.NetCore10._0_QR_Restaurant_Order.UI.Controllers;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models
{
    public class NotificationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public NotificationType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
    }
}
