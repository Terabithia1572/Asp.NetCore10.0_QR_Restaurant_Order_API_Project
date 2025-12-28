using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
