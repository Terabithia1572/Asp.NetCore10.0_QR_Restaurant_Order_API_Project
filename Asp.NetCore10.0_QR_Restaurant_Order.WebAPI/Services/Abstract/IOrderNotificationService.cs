using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Services.Abstract
{
    public interface IOrderNotificationService
    {
        Task SendOrderCreatedAsync(Order order);
        Task SendOrderStatusChangedAsync(Order order);
    }
}
