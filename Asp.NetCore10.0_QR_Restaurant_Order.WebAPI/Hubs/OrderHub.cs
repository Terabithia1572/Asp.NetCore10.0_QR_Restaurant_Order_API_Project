// QRRestaurantOrder.API/Hubs/OrderHub.cs
using Microsoft.AspNetCore.SignalR;

namespace QRRestaurantOrder.API.Hubs
{
    // Bu hub üzerinden mutfak, garson, kasa, admin gibi rollere gerçek zamanlı bildirim gidecek
    public class OrderHub : Hub
    {
        private const string KitchenGroup = "Kitchen";
        private const string WaiterGroup = "Waiter";
        private const string AdminGroup = "Admin";

        // 🔹 Mutfak ekranı bağlandığında bu gruba girsin
        public async Task JoinKitchen()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, KitchenGroup);
        }

        // 🔹 Garson / servis ekranı bağlandığında bu gruba girsin
        public async Task JoinWaiter()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, WaiterGroup);
        }

        // 🔹 Admin dashboard bağlandığında bu gruba girsin
        public async Task JoinAdmin()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, AdminGroup);
        }

        // 🔹 Belirli masa için grup (ileride işimize çok yarar)
        public async Task JoinTable(string tableCode)
        {
            if (!string.IsNullOrWhiteSpace(tableCode))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Table_{tableCode}");
            }
        }

        // İstersen client’ların tetiklediği metodlar da yazabiliriz,
        // ama ana mantık: server tarafı IHubContext ile bu hub’a bildirim basacak.
    }
}
