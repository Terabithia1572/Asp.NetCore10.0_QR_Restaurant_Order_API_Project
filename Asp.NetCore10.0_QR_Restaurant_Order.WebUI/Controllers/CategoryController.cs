using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.CategoryDTOs;
// UI katmanında API’den gelen kategori verilerini karşılamak için kullanılan DTO sınıflarını ekliyoruz

using Microsoft.AspNetCore.Mvc;
// MVC yapısının temel bileşenleri (Controller, IActionResult, View vb.) için gerekli

using Newtonsoft.Json;
// API’den gelen JSON verisini C# nesnelerine dönüştürmek (Deserialize) için kullanılıyor

using System.Threading.Tasks;
// async / await yapısını kullanabilmek için gerekli namespace

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Controllers
{
    // Category işlemlerini yöneten MVC Controller
    // Bu controller API'den veri alır ve Razor View'lara gönderir
    public class CategoryController : Controller
    {
        // IHttpClientFactory, HttpClient nesnelerini güvenli ve performanslı şekilde
        // oluşturmak için Dependency Injection ile kullanılan modern yaklaşımdır
        private readonly IHttpClientFactory _httpClientFactory;

        // Constructor üzerinden IHttpClientFactory inject edilir
        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // Kategori listesini getiren action metodu
        // async kullanımı sayesinde API çağrısı thread’i kilitlemez
        public async Task<IActionResult> CategoryList()
        {
            // IHttpClientFactory üzerinden bir HttpClient instance’ı oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // WebAPI projesindeki Categories endpoint’ine GET isteği atıyoruz
            var responseMessage = await client.GetAsync("https://localhost:7074/api/Categories");

            // API’den dönen response başarılıysa devam ediyoruz
            if (responseMessage != null)
            {
                // Response içeriğini JSON string olarak okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON verisini ResultCategoryDTO listesine deserialize ediyoruz
                // Böylece View tarafında güçlü tip (strongly typed) model kullanabiliyoruz
                var values = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(jsonData);

                // DTO listesini View’a gönderiyoruz
                return View(values);
            }

            // API çağrısı başarısız olursa View boş şekilde döner
            return View();
        }
    }
}
