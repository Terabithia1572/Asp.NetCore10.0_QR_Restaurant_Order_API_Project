using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.ViewComponents._AdminLayoutComponents
{
    public class _AdminLayoutHeadComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
