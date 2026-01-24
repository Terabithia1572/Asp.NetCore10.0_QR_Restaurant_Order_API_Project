
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Hubs;
using Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Services.Abstract;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Services.Concrete
{
    public class OrderNotificationService : IOrderNotificationService
    {
        private readonly IHubContext<SignalRHub> _hubContext;

        public OrderNotificationService(IHubContext<SignalRHub> hubContext)
        {
            _hubContext = hubContext;
        }

        // ✅ Yeni sipariş oluşturulunca çağırılacak
        public async Task SendOrderCreatedAsync(Order order)
        {
            // Burada TableCode yerine TableID kullanıyoruz
            var dto = new
            {
                OrderId = order.OrderID,
                TableId = order.TableID,
                CustomerName = order.CustomerName,
                TotalPrice = order.TotalPrice,
                GuestCount = order.GuestCount,
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                ChangedAt = DateTime.Now
            };

            // Şimdilik basit olsun: tüm client’lara gönder
            await _hubContext.Clients.All
                .SendAsync("OrderCreated", dto);
        }

        // ✅ Sipariş statüsü değişince çağırılacak
        public async Task SendOrderStatusChangedAsync(Order order)
        {
            // OrderStatus muhtemelen int → ona göre mapliyoruz
            string statusDisplayName = order.OrderStatus switch
            {
                0 => "Sipariş Oluşturuldu",
                1 => "Sipariş Onaylandı",
                2 => "Sipariş Hazırlanıyor",
                3 => "Sipariş Hazır",
                4 => "Sipariş Servis Edildi",
                5 => "Sipariş İptal Edildi",
                _ => "Sipariş Güncellendi"
            };

            var dto = new
            {
                OrderId = order.OrderID,
                TableId = order.TableID,
                TotalPrice = order.TotalPrice,          // 🔥 EKLENDİ
                OrderStatus = order.OrderStatus,
                PaymentStatus = order.PaymentStatus,
                StatusDisplayName = statusDisplayName,
                ChangedAt = DateTime.Now
            };

            await _hubContext.Clients.All
                .SendAsync("OrderStatusChanged", dto);
        }
    }
}
