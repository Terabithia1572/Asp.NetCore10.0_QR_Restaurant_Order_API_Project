using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.OrderDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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

        public OrdersController(
            IOrderService orderService,
            IOrderDetailService orderDetailService,
            IDashboardService dashboardService,
            IHubContext<SignalRHub> hubContext)
        {
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _dashboardService = dashboardService;
            _hubContext = hubContext;
        }

        // Örnek: /api/Orders
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 1) Buraya senin mevcut sipariş oluşturma mantığını koyacağız
            // -------------------------------------------------------------
            // Örnek iskelet (senin entity ve DTO’larına göre uyarlayacaksın):

            // var order = new Order
            // {
            //     TableID = dto.TableID,
            //     CustomerName = dto.CustomerName,
            //     TotalPrice = dto.TotalPrice,
            //     GuestCount = dto.GuestCount,
            //     CreatedDate = DateTime.Now,
            //     OrderStatus = dto.OrderStatus,
            //     PaymentStatus = dto.PaymentStatus
            // };
            //
            // _orderService.TAdd(order);
            //
            // foreach (var item in dto.OrderDetails)
            // {
            //     var detail = new OrderDetail
            //     {
            //         OrderID = order.OrderID,
            //         ProductID = item.ProductID,
            //         Quantity = item.Quantity,
            //         UnitPrice = item.UnitPrice
            //     };
            //     _orderDetailService.TAdd(detail);
            // }

            // 2) Sipariş kaydı tamamlandıktan sonra güncel dashboard özetini al
            var summary = await _dashboardService.GetDashboardSummaryAsync();

            // 3) Tüm bağlı admin panellerine gönder
            await _hubContext.Clients.All.SendAsync("ReceiveDashboardSummary", summary);

            return Ok(new { message = "Order created and dashboard updated." });
        }
    }
}
