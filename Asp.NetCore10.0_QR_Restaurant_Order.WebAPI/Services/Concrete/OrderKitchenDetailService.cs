using Asp.NetCore10._0_QR_Restaurant_Order.DataAccessLayer.Concrete;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Services.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Services.Concrete
{
    public class OrderKitchenDetailService : IOrderKitchenDetailService
    {
        // 🔴 BURAYI KENDİ DbContext SINIF ADINA GÖRE DÜZENLE
        private readonly SignalRContext _context;

        public OrderKitchenDetailService(SignalRContext context)
        {
            _context = context;
        }

        // 🔹 1) Detay + Include (Kitchen modal için)
        public async Task<Order> GetOrderWithDetailsAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Product)
                .FirstOrDefaultAsync(o => o.OrderID == orderId);
        }

        // 🔹 2) Status güncelle (Kitchen butonlarının vurduğu yer)
        public async Task UpdateStatusAsync(int orderId, int status)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderID == orderId);
            if (order == null)
                return;

            order.OrderStatus = status; // property adın farklıysa ona göre değiştir

            // İstersen UpdatedDate vs. alanlarını da set edebilirsin:
            // order.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        // 🔹 3) Order var mı yok mu? (Guard / kontrol amaçlı)
        public async Task<bool> ExistsAsync(int orderId)
        {
            return await _context.Orders.AnyAsync(o => o.OrderID == orderId);
        }

        // 🔹 4) Kitchen aktif siparişler (kitchen-active endpoint’ine temel)
        public async Task<List<Order>> GetActiveKitchenOrdersAsync()
        {
            // Buradaki filtreyi kendi status mantığına göre ayarlayabilirsin.
            // Örn: 4 = Served, 5 = Canceled → mutfakta görünmesin.
            return await _context.Orders
                .Where(o => o.OrderStatus != 4 && o.OrderStatus != 5)
                .Include(o => o.OrderDetails)
                    .ThenInclude(d => d.Product)
                .OrderByDescending(o => o.CreatedDate) // alan adın farklı olabilir
                .ToListAsync();
        }
    }
}
