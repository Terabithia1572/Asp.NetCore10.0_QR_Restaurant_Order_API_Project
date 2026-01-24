using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.OrderDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Hubs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // ... mevcut ctor alanların ...
        private readonly IDashboardService _dashboardService;
        private readonly IHubContext<DashboardHub> _hubContext;

        public OrdersController(IDashboardService dashboardService, IHubContext<DashboardHub> hubContext)
        {
            _dashboardService = dashboardService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO dto)
        {
            // 1) Siparişi kaydet (Order + OrderDetails)
            // ...

            // 2) Güncel dashboard verisini al
            var summary = await _dashboardService.GetDashboardSummaryAsync();

            // 3) Tüm bağlı admin panellerine gönder
            await _hubContext.Clients.All.SendAsync("ReceiveDashboardSummary", summary);

            return Ok();
        }
    }
}
