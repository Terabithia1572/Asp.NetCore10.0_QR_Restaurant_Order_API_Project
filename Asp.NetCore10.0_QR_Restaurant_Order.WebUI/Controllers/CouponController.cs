using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Asp.NetCore10._0_QR_Restaurant_Order.UI.Controllers
{
    public class CouponController : Controller
    {
        public IActionResult Index()
        {
            var coupons = GetSampleCoupons();
            return View("List", coupons);
        }

        private static List<CouponViewModel> GetSampleCoupons()
        {
            return new List<CouponViewModel>
            {
                new CouponViewModel { Id = 1, Code = "WELCOME50", Discount = 50, Type = "Fixed", UsageLimit = 100, Used = 14, Status = true },
                new CouponViewModel { Id = 2, Code = "FIRSTORDER", Discount = 25, Type = "%", UsageLimit = 500, Used = 310, Status = true },
                new CouponViewModel { Id = 3, Code = "VIP2026", Discount = 15, Type = "%", UsageLimit = 30, Used = 2, Status = false },
            };
        }
    }

    
}
