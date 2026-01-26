using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Asp.NetCore10._0_QR_Restaurant_Order.UI.Controllers
{
    public class TableController : Controller
    {
        // Şimdilik dummy veri – ileride DB veya API'ye bağlanırız
        private static List<TableStatusViewModel> GetSampleTables()
        {
            return new List<TableStatusViewModel>
            {
                new TableStatusViewModel { TableID = 1, Name = "Masa 1", Capacity = 2, Status = "Boş" },
                new TableStatusViewModel { TableID = 2, Name = "Masa 2", Capacity = 4, Status = "Dolu" },
                new TableStatusViewModel { TableID = 3, Name = "Masa 3", Capacity = 4, Status = "Hesap İstendi" },
                new TableStatusViewModel { TableID = 4, Name = "Masa 4", Capacity = 6, Status = "Rezerve" }
            };
        }

        // /Table/Status  → Masa Durumu ekranı
        public IActionResult Status()
        {
            var tables = GetSampleTables();
            return View(tables);
        }

        // /Table/QrManagement → QR Yönetimi ekranı
        public IActionResult QrManagement()
        {
            var tables = GetSampleTables();

            // Her masa için örnek bir QR URL üretelim (ileride gerçek link olacak)
            var model = new List<TableQRViewModel>();
            foreach (var t in tables)
            {
                model.Add(new TableQRViewModel
                {
                    TableID = t.TableID,
                    Name = t.Name,
                    Capacity = t.Capacity,
                    QRUrl = $"/RestaurantUI/Index?table={t.TableID}" // ileride gerçek QR-entry URL'si
                });
            }

            return View(model);
        }
    }

   
}
