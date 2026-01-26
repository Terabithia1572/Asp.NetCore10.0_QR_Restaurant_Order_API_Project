using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Asp.NetCore10._0_QR_Restaurant_Order.UI.Controllers
{
    public class ExtraController : Controller
    {
        // Şimdilik dummy veriler – ileride DB & WebAPI'ye bağlanacak
        private static List<ExtraItemViewModel> GetSampleExtras()
        {
            return new List<ExtraItemViewModel>
            {
                new ExtraItemViewModel
                {
                    Id = 1,
                    Name = "Ekstra Peynir",
                    Category = "Pizza",
                    Price = 35m,
                    IsActive = true
                },
                new ExtraItemViewModel
                {
                    Id = 2,
                    Name = "Acı Sos",
                    Category = "Genel",
                    Price = 15m,
                    IsActive = true
                },
                new ExtraItemViewModel
                {
                    Id = 3,
                    Name = "Büyük Boy",
                    Category = "İçecek",
                    Price = 20m,
                    IsActive = false
                },
                new ExtraItemViewModel
                {
                    Id = 4,
                    Name = "Glutensiz Hamur",
                    Category = "Pizza",
                    Price = 50m,
                    IsActive = true
                }
            };
        }

        // /Extra/Index → Ekstra / Opsiyonlar listesi
        public IActionResult Index()
        {
            var extras = GetSampleExtras();
            return View(extras);
        }
    }

 
}
