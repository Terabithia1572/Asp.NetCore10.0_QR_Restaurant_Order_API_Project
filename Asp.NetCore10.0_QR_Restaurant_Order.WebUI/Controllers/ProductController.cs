using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.ProductDTOs;
// UI katmanında ürün + kategori adını birlikte göstermek için kullanılan DTO

using Microsoft.AspNetCore.Mvc;
// MVC Controller, IActionResult, View yapıları için gerekli

using Newtonsoft.Json;
// API'den gelen JSON verisini C# nesnesine dönüştürmek için

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Controllers
{
    // Ürün işlemlerini yöneten MVC Controller
    // Bu controller ürünleri API'den çeker ve View'a gönderir
    public class ProductController : Controller
    {
        // HttpClientFactory modern ve güvenli HttpClient kullanımı için
        private readonly IHttpClientFactory _httpClientFactory;

        // API base adresi (tek noktadan yönetim için)
        private const string ApiBaseUrl = "https://localhost:7074/api/Products";

        // Constructor – Dependency Injection
        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // =====================================
        // PRODUCT LIST WITH CATEGORY NAME
        // =====================================
        // Ürünleri kategori adlarıyla birlikte listeleyen action
        public async Task<IActionResult> ProductList()
        {
            // HttpClient instance oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // API tarafında Product + Category join yapan endpoint
            // Örn: GET /api/Products/GetProductsWithCategory
            var responseMessage = await client.GetAsync($"{ApiBaseUrl}/with-category");

            // API başarılı döndüyse
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON veriyi string olarak okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON → ResultProductWithCategoryDTO listesine çeviriyoruz
                var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDTO>>(jsonData);

                // View'a DTO listesini gönderiyoruz
                return View(values);
            }

            // API başarısızsa boş liste ile sayfa açılır
            return View(new List<ResultProductWithCategoryDTO>());
        }
    }
}
