using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Asp.NetCore10._0_QR_Restaurant_Order.UI.Controllers
{
    public class CustomerController : Controller
    {
        // Şimdilik dummy veriler – ileride WebAPI + DB ile değiştiririz
        private static readonly List<CustomerViewModel> _customers = new()
        {
            new CustomerViewModel
            {
                Id = 1,
                FullName = "Ahmet Yılmaz",
                Phone = "+90 532 000 00 01",
                Email = "ahmet.yilmaz@example.com",
                TotalOrders = 8,
                TotalSpend = 2450m,
                LastOrderDate = DateTime.Now.AddDays(-2),
                Tags = new List<string> { "VIP", "Regular" },
                Notes = "Steak ve şarap tercih ediyor. Hafta sonu akşamları sık gelir."
            },
            new CustomerViewModel
            {
                Id = 2,
                FullName = "Zeynep Demir",
                Phone = "+90 532 000 00 02",
                Email = "zeynep.demir@example.com",
                TotalOrders = 3,
                TotalSpend = 720m,
                LastOrderDate = DateTime.Now.AddDays(-10),
                Tags = new List<string> { "New" },
                Notes = "Son rezervasyonda doğum günü organizasyonu yaptı."
            },
            new CustomerViewModel
            {
                Id = 3,
                FullName = "Mehmet Kaya",
                Phone = "+90 532 000 00 03",
                Email = "mehmet.kaya@example.com",
                TotalOrders = 15,
                TotalSpend = 6120m,
                LastOrderDate = DateTime.Now.AddDays(-1),
                Tags = new List<string> { "VIP", "High Value" },
                Notes = "Kurumsal misafirlerle geliyor. Fiks menü ve şarap eşleşmesi seviyor."
            }
        };

        // LISTE – /Customer/Index
        public IActionResult Index()
        {
            // AvgTicket hesaplayalım
            var model = _customers.Select(c =>
            {
                c.AverageTicket = c.TotalOrders > 0
                    ? Math.Round(c.TotalSpend / c.TotalOrders, 2)
                    : 0;
                return c;
            }).ToList();

            return View(model);
        }

        // DETAY – /Customer/Detail/1
        public IActionResult Detail(int id)
        {
            var customer = _customers.FirstOrDefault(x => x.Id == id);
            if (customer == null)
                return NotFound();

            customer.AverageTicket = customer.TotalOrders > 0
                ? Math.Round(customer.TotalSpend / customer.TotalOrders, 2)
                : 0;

            return View(customer);
        }
    }

    // A2 CRM MODELI
    
}
