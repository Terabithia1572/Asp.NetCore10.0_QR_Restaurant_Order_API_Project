using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Concrete;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.DashboardDTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Concrete
{
    public class DashboardManager : IDashboardService
    {
        private readonly SignalRContext _context;

        public DashboardManager(SignalRContext context)
        {
            _context = context;
        }

        public async Task<ResultDashboardSummaryDTO> GetDashboardSummaryAsync()
        {
            var today = DateTime.Today;
            var monthStart = new DateTime(today.Year, today.Month, 1);

            var todayOrders = _context.Orders.Where(o => o.CreatedDate >= today);
            var monthOrders = _context.Orders.Where(o => o.CreatedDate >= monthStart);

            var dto = new ResultDashboardSummaryDTO
            {
                TodayRevenue = await todayOrders.SumAsync(o => (decimal?)o.TotalPrice) ?? 0,
                TodayOrderCount = await todayOrders.CountAsync(),
                MonthlyRevenue = await monthOrders.SumAsync(o => (decimal?)o.TotalPrice) ?? 0,
                MonthlyOrderCount = await monthOrders.CountAsync(),
                TotalOrderCount = await _context.Orders.CountAsync(),
                TotalGuestCount = await _context.Orders.SumAsync(o => (int?)o.GuestCount) ?? 0,
                ActiveTableCount = await _context.Tables.CountAsync(t => t.Status == true)
            };

            dto.TopProducts = await _context.OrderDetails
                .Include(od => od.Product)
                .GroupBy(od => new { od.ProductID, od.Product.ProductName, od.Product.ProductImageURL })
                .Select(g => new ResultTopProductDTO
                {
                    ProductName = g.Key.ProductName,
                    ImageUrl = g.Key.ProductImageURL,
                    TotalQuantity = g.Sum(x => x.Quantity),
                    TotalAmount = g.Sum(x => x.Quantity * x.UnitPrice)
                })
                .OrderByDescending(x => x.TotalQuantity)
                .Take(6)
                .ToListAsync();

            dto.RecentOrders = await _context.Orders
                .Include(o => o.Table)
                .OrderByDescending(o => o.CreatedDate)
                .Take(10)
                .Select(o => new ResultRecentOrderDTO
                {
                    OrderID = o.OrderID,
                    TableName = o.Table.TableName,
                    CustomerName = o.CustomerName,
                    CreatedDate = o.CreatedDate,
                    TotalPrice = o.TotalPrice,
                    Profit = 0,
                    StatusText = o.OrderStatus.ToString()
                })
                .ToListAsync();

            return dto;
        }
    }
}
