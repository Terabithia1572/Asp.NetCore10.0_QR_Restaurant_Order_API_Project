using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
