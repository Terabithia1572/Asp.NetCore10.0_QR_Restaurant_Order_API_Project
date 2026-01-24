using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.OrderDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Hubs;
using Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IDashboardService _dashboardService;
        private readonly IHubContext<SignalRHub> _hubContext;
        private readonly IOrderNotificationService _orderNotificationService;

        public OrdersController(
            IOrderService orderService,
            IOrderDetailService orderDetailService,
            IDashboardService dashboardService,
            IHubContext<SignalRHub> hubContext,
            IOrderNotificationService orderNotificationService)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _dashboardService = dashboardService;
            _hubContext = hubContext;
            _orderNotificationService = orderNotificationService;
        }

        // POST: /api/Orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1) Sipariş entity'sini oluştur
            var order = new Order
            {
                TableID = dto.TableID,
                CustomerName = dto.CustomerName,
                TotalPrice = dto.TotalPrice,
                GuestCount = dto.GuestCount,
                CreatedDate = DateTime.Now,
                OrderStatus = dto.OrderStatus,
                PaymentStatus = dto.PaymentStatus
            };

            // 2) Önce order'ı kaydet ki OrderID oluşsun
            _orderService.TAdd(order);

            // 3) Detayları ekle
            if (dto.OrderDetails != null)
            {
                foreach (var item in dto.OrderDetails)
                {
                    var detail = new OrderDetail
                    {
                        OrderID = order.OrderID,
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    };

                    _orderDetailService.TAdd(detail);
                }
            }

            // 4) Order bazlı SignalR bildirimi (mutfak / garson / admin)
            await _orderNotificationService.SendOrderCreatedAsync(order);

            // 5) Dashboard özetini güncelle ve admin dashboard'a gönder
            var summary = await _dashboardService.GetDashboardSummaryAsync();
            await _hubContext.Clients.All
                .SendAsync("ReceiveDashboardSummary", summary);

            return Ok(new
            {
                message = "Order created, notifications sent and dashboard updated.",
                orderId = order.OrderID
            });
        }

        // ÖRNEK: Sipariş durumunu değiştiren endpoint
        // PUT: /api/Orders/{id}/status
        [HttpPut("{id}/status")]
        public async Task<IActionResult> ChangeOrderStatus(int id, [FromBody] UpdateOrderStatusDTO dto)
        {
            var order = _orderService.TGetByID(id);
            if (order == null)
                return NotFound(new { message = "Order not found." });

            order.OrderStatus = dto.OrderStatus;
            _orderService.TUpdate(order);

            // Order status değişince SignalR ile bildir
            await _orderNotificationService.SendOrderStatusChangedAsync(order);

            // Dashboard'u tekrar güncelle
            var summary = await _dashboardService.GetDashboardSummaryAsync();
            await _hubContext.Clients.All
                .SendAsync("ReceiveDashboardSummary", summary);

            return Ok(new { message = "Order status updated.", orderId = order.OrderID });
        }
        // GET: /api/Orders/kitchen-active
        // Mutfak ekranı açıldığında ilk yüklemede çağrılacak
        [HttpGet("kitchen-active")]
        public IActionResult GetKitchenActiveOrders()
        {
            var orders = _orderService.TGetListAll()
                .Where(x => x.OrderStatus != 4 && x.OrderStatus != 5)        // Servis Edildi / İptal hariç
                .OrderByDescending(x => x.CreatedDate)                       // 🔥 En yeni sipariş en üstte
                .ThenByDescending(x => x.OrderID)                            // Aynı anda gelenleri ID'ye göre sıralar
                .ToList();

            var result = orders.Select(x =>
            {
                string statusText = x.OrderStatus switch
                {
                    0 => "Sipariş Oluşturuldu",
                    1 => "Sipariş Onaylandı",
                    2 => "Sipariş Hazırlanıyor",
                    3 => "Sipariş Hazır",
                    4 => "Sipariş Servis Edildi",
                    5 => "Sipariş İptal Edildi",
                    _ => "Bilinmeyen Durum"
                };

                return new KitchenOrderResultDTO
                {
                    OrderID = x.OrderID,
                    TableID = x.TableID,
                    CustomerName = x.CustomerName,
                    TotalPrice = x.TotalPrice,
                    GuestCount = x.GuestCount,
                    OrderStatus = x.OrderStatus,
                    CreatedDate = x.CreatedDate,
                    StatusText = statusText
                };
            }).ToList();

            return Ok(result);
        }


    }
}
