using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Services.Abstract
{
    public interface IOrderKitchenDetailService
    {
        Task<Order> GetOrderWithDetailsAsync(int orderId);
        Task UpdateStatusAsync(int orderId, int status);
        Task<bool> ExistsAsync(int orderId);
        Task<List<Order>> GetActiveKitchenOrdersAsync();

    }
}
