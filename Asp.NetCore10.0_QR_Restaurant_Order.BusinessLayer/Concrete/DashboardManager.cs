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

            // Bugünün siparişleri (sadece bugün)
            var todayOrdersQuery = _context.Orders
                .Where(o => o.CreatedDate >= today);

            // Bu ayın siparişleri (ayın başından bugüne kadar)
            var monthOrdersQuery = _context.Orders
                .Where(o => o.CreatedDate >= monthStart);

            var dto = new ResultDashboardSummaryDTO
            {
                // Bugünkü ciro
                TodayRevenue = await todayOrdersQuery.SumAsync(o => (decimal?)o.TotalPrice) ?? 0,

                // Bugünkü sipariş sayısı
                TodayOrderCount = await todayOrdersQuery.CountAsync(),

                // Bu ayın toplam cirosu
                MonthlyRevenue = await monthOrdersQuery.SumAsync(o => (decimal?)o.TotalPrice) ?? 0,

                // Bu ayın sipariş sayısı
                MonthlyOrderCount = await monthOrdersQuery.CountAsync(),

                // Sistem boyunca toplam sipariş sayısı
                TotalOrderCount = await _context.Orders.CountAsync(),

                // Tüm siparişlerdeki toplam misafir sayısı
                TotalGuestCount = await _context.Orders.SumAsync(o => (int?)o.GuestCount) ?? 0,

                // Şu anda aktif olan masa sayısı
                ActiveTableCount = await _context.Tables.CountAsync(t => t.Status == true)
            };

            // En çok satan ürünler
            dto.TopProducts = await _context.OrderDetails
                .Include(od => od.Product)
                .GroupBy(od => new
                {
                    od.ProductID,
                    od.Product.ProductName,
                    od.Product.ProductImageURL
                })
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

            // Son siparişler (dashboard altındaki tablo)
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
                    Profit = 0, // maliyet eklediğimizde hesaplanabilir
                    StatusText = o.OrderStatus.ToString()
                })
                .ToListAsync();

            return dto;
        }
    }
}
