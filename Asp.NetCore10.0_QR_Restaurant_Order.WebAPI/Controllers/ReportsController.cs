using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Concrete;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.ReportDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly SignalRContext _context;

        public ReportsController(SignalRContext context)
        {
            _context = context;
        }

        // GET api/Reports/Statistics
        [HttpGet("Statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            var now = DateTime.Now;
            var today = now.Date;
            var weekStart = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var monthStart = new DateTime(today.Year, today.Month, 1);
            var last30Days = today.AddDays(-29);

            var baseQuery = _context.Orders.AsNoTracking();

            // 4 = Servis edildi, PaymentStatus = true (başarılı)
            var completedRevenueQuery = baseQuery
                .Where(o => o.OrderStatus == 4 && o.PaymentStatus == true);

            var trafficQuery = baseQuery
                .Where(o => o.OrderStatus != 5); // 5 = iptal

            var completedOrders = await completedRevenueQuery.ToListAsync();

            var summary = new RevenueSummaryDTO();

            if (completedOrders.Any())
            {
                summary.TotalRevenue = completedOrders.Sum(o => o.TotalPrice);
                summary.TotalOrders = completedOrders.Count;
                summary.TodayRevenue = completedOrders
                    .Where(o => o.CreatedDate.Date == today)
                    .Sum(o => o.TotalPrice);
                summary.TodayOrders = completedOrders
                    .Count(o => o.CreatedDate.Date == today);
                summary.ThisWeekRevenue = completedOrders
                    .Where(o => o.CreatedDate.Date >= weekStart)
                    .Sum(o => o.TotalPrice);
                summary.ThisMonthRevenue = completedOrders
                    .Where(o => o.CreatedDate.Date >= monthStart)
                    .Sum(o => o.TotalPrice);

                var tableCount = completedOrders.Select(o => o.TableID).Distinct().Count();
                if (tableCount > 0)
                    summary.AveragePerTable = summary.TotalRevenue / tableCount;

                if (summary.TotalOrders > 0)
                    summary.AverageTicketAmount = summary.TotalRevenue / summary.TotalOrders;
            }

            var revenueLast30Days = await completedRevenueQuery
                .Where(o => o.CreatedDate.Date >= last30Days)
                .GroupBy(o => o.CreatedDate.Date)
                .Select(g => new RevenueChartPointDTO
                {
                    Date = g.Key,
                    Total = g.Sum(x => x.TotalPrice)
                })
                .OrderBy(x => x.Date)
                .ToListAsync();

            var peakHours = await trafficQuery
                .GroupBy(o => o.CreatedDate.Hour)
                .Select(g => new PeakHourDTO
                {
                    Hour = g.Key,
                    OrderCount = g.Count()
                })
                .OrderBy(x => x.Hour)
                .ToListAsync();

            var vm = new ReportsDTO
            {
                RevenueSummary = summary,
                RevenueLast30Days = revenueLast30Days,
                PeakHours = peakHours,
                TopProducts = new List<ProductPerformanceDTO>() // ileride dolduracağız
            };

            return Ok(vm);
        }
    }
}
