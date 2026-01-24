using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.DashboardDTOs;
// Dashboard ekranında kullanacağımız özet istatistik DTO'su

using Microsoft.AspNetCore.Mvc;
// MVC Controller, IActionResult, View gibi temel bileşenler için gerekli

using Newtonsoft.Json;
// İleride API'den JSON çekip deserialize etmek için (şimdilik ihtiyaç olmasa da hazır dursun)

using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
// async/await ve HttpClient kullanım senaryoları için gerekli namespace'ler

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Controllers
{
    // QR Restaurant projesinin Admin tarafındaki
    // Dashboard (ana yönetim ekranı) işlemlerini yöneten Controller
    public class DashboardController : Controller
    {
        // IHttpClientFactory:
        // - HttpClient örneklerini yönetilebilir ve performanslı şekilde üretmek için
        // - API ile konuşurken modern ve tavsiye edilen yöntemdir
        private readonly IHttpClientFactory _httpClientFactory;

        // İleride Dashboard verilerini API'den çekeceğimiz base adres
        // Örn: https://localhost:7074/api/Dashboard/Summary
        // Şimdilik sadece örnek olarak burada dursun, endpoint hazır olunca aktif edeceğiz.
        private const string DashboardSummaryApiUrl = "https://localhost:7074/api/Dashboard/Summary";

        // Constructor üzerinden IHttpClientFactory bağımlılığını enjekte ediyoruz
        public DashboardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // ============================
        // 1) ADMIN DASHBOARD (GET)
        // ============================
        // Admin paneline giriş yapıldığında ilk açılacak ekran.
        // Burada:
        // - Bugünkü sipariş sayısı
        // - Bugünkü ciro
        // - Aktif masa sayısı
        // - Ortalama servis süresi
        // - QR tarama sayısı
        // - Yeni müşteri sayısı
        // gibi istatistikleri göstereceğiz.
        public async Task<IActionResult> Index()
        {
            // NOT:
            // Şu anda API tarafında Dashboard endpoint'leri hazır olmadığı için
            // demo amaçlı olarak verileri Controller içinde "mock" (örnek) olarak dolduruyoruz.
            // API hazır olunca aşağıdaki sabit veriler yerine HttpClient ile API'den çekeceğiz.

            // ============================
            // A) GEÇİCİ (MOCK) VERİ OLUŞTURMA
            // ============================
            var summaryModel = new ResultDashboardSummaryDTO
            {
                TodayTotalOrderCount = 128,   // Örnek: bugün alınan toplam sipariş
                TodayTotalRevenue = 24580, // Örnek: bugünkü ciro (₺)
                ActiveTableCount = 24,    // Örnek: şu an aktif (açık) masa
                AverageServiceTimeMinute = 14,    // Örnek: ortalama servis süresi (dk)
                TodayQrScanCount = 870,   // Örnek: bugün QR ile menü tarama sayısı
                TodayNewCustomerCount = 12     // Örnek: bugün ilk kez gelen müşteri
            };

            // ============================
            // B) İLERİDE API'DEN VERİ ÇEKME (ŞABLON)
            // ============================
            // Aşağıdaki kod bloğu, Dashboard Summary verisini API'den çekmek için kullanılacak
            // hazır bir şablondur. Endpoint hazır olduğunda üstteki mock veriyi kaldırıp
            // bu kısmı aktif hale getirebilirsin.

            /*
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync(DashboardSummaryApiUrl);

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                summaryModel = JsonConvert.DeserializeObject<ResultDashboardSummaryDTO>(jsonData);
            }
            */

            // View tarafına strongly-typed model ile dönüyoruz
            return View(summaryModel);
        }
    }
}
