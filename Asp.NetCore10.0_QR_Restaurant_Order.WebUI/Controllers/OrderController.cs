using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.KitchenOrderDetailDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.OrderDTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Asp.NetCore10._0_QR_Restaurant_Order.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://localhost:7074/api/Orders";

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        // GENEL LISTE (Hepsi) – Sol menüde "Siparişler" ana maddesi buraya gider
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetAsync($"{ApiBaseUrl}/kitchen-active");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Sipariş listesi alınırken bir hata oluştu.";
                return View(new List<KitchenOrderResultDTO>());
            }

            var orders = await response.Content.ReadFromJsonAsync<List<KitchenOrderResultDTO>>();
            ViewData["Title"] = "Tüm Aktif Siparişler";
            return View("List", orders);
        }

        // YARDIMCI: belirli status'e göre liste
        private async Task<IActionResult> ListByStatusAsync(int status, string title)
        {
            var response = await _httpClient.GetAsync($"{ApiBaseUrl}/status/{status}");
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Sipariş listesi alınırken bir hata oluştu.";
                return View("List", new List<KitchenOrderResultDTO>());
            }

            var orders = await response.Content.ReadFromJsonAsync<List<KitchenOrderResultDTO>>();
            ViewData["Title"] = title;
            return View("List", orders);
        }

        // /Order/NewOrders  → Yeni Siparişler (status = 0)
        public Task<IActionResult> NewOrders()
            => ListByStatusAsync(0, "Yeni Siparişler");

        // /Order/Preparing  → Hazırlanıyor (status = 2)
        public Task<IActionResult> Preparing()
            => ListByStatusAsync(2, "Hazırlanan Siparişler");

        // /Order/Completed  → Tamamlandı (Servis Edildi = 4)
        public Task<IActionResult> Completed()
            => ListByStatusAsync(4, "Tamamlanan Siparişler");

        // /Order/Cancelled  → İptal / İade (status = 5)
        public Task<IActionResult> Cancelled()
            => ListByStatusAsync(5, "İptal / İade Siparişler");

        // DETAY
        public async Task<IActionResult> Detail(int id)
        {
            var response = await _httpClient.GetAsync($"{ApiBaseUrl}/{id}/detail");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var detail = await response.Content.ReadFromJsonAsync<KitchenOrderDetailDTO>();
            return View(detail);
        }

        // DURUM GÜNCELLEME (Admin tarafından manuel değiştirmek istersen)
        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int id, int nextStatus)
        {
            var body = new { OrderStatus = nextStatus };
            var response = await _httpClient.PutAsJsonAsync($"{ApiBaseUrl}/{id}/status", body);

            if (!response.IsSuccessStatusCode)
                TempData["OrderStatusError"] = "Durum güncellenirken bir hata oluştu.";
            else
                TempData["OrderStatusSuccess"] = "Sipariş durumu güncellendi.";

            return RedirectToAction(nameof(Index));
        }
    }
}
