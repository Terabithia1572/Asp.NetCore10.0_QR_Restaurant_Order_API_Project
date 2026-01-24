using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly IDashboardService _dashboardService;

        public SignalRHub(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// İstemci (admin panel) bağlandığında ilk dashboard verisini gönder.
        /// </summary>
        public override async Task OnConnectedAsync()
        {
            await SendDashboardSummary();
            await base.OnConnectedAsync();
        }

        /// <summary>
        /// İsteyen herhangi bir client bu metodu çağırarak dashboard verisini tazeleyebilir.
        /// </summary>
        public async Task SendDashboardSummary()
        {
            var summary = await _dashboardService.GetDashboardSummaryAsync();
            await Clients.All.SendAsync("ReceiveDashboardSummary", summary);
        }
    }
}
