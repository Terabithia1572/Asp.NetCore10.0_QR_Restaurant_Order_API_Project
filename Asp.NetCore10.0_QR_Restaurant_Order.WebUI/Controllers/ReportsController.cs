using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Models.ReportViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public ReportsController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            // appsettings.json içine API için bir baseUrl koyarsın:
            // "QrApiBaseUrl": "https://localhost:7074"
            var baseUrl = "https://localhost:7074";

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(baseUrl);

            var response = await client.GetAsync("api/Reports/Statistics");

            if (!response.IsSuccessStatusCode)
            {
                // Basit fallback
                return View(new ReportsViewModel());
            }

            var jsonData = await response.Content.ReadAsStringAsync();

            var dto = JsonConvert.DeserializeObject<ReportsViewModel>(jsonData);

            // ViewModel ve DTO field’ları aynı isimde olursa direkt deserialize olur.
            return View(dto);
        }
    }
}
