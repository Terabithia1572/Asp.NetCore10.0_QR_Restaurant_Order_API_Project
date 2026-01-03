using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.CategoryDTOs;
// UI katmanında API’den gelen kategori verilerini karşılamak için kullanılan DTO sınıflarını ekliyoruz

using Microsoft.AspNetCore.Mvc;
// MVC yapısının temel bileşenleri (Controller, IActionResult, View vb.) için gerekli

using Newtonsoft.Json;
// API’den gelen JSON verisini C# nesnelerine dönüştürmek (Deserialize) için kullanılıyor

using System.Text;
// POST/PUT isteklerinde JSON body gönderebilmek için (StringContent) gerekli

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Controllers
{
    // Category işlemlerini yöneten MVC Controller
    // Bu controller API'den veri alır ve Razor View'lara gönderir
    public class CategoryController : Controller
    {
        // IHttpClientFactory, HttpClient nesnelerini güvenli ve performanslı şekilde
        // oluşturmak için Dependency Injection ile kullanılan modern yaklaşımdır
        private readonly IHttpClientFactory _httpClientFactory;

        // API base adresini tek yerde tutmak ileride değiştirmeyi kolaylaştırır
        private const string ApiBaseUrl = "https://localhost:7074/api/Categories";

        // Constructor üzerinden IHttpClientFactory inject edilir
        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // ============================
        // 1) CATEGORY LIST (GET)
        // ============================
        public async Task<IActionResult> CategoryList()
        {
            // IHttpClientFactory üzerinden bir HttpClient instance’ı oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // WebAPI projesindeki Categories endpoint’ine GET isteği atıyoruz
            var responseMessage = await client.GetAsync(ApiBaseUrl);

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

        // ============================
        // 2) CREATE CATEGORY (GET)
        // ============================
        // Create formunu ekrana basmak için GET action
        [HttpGet]
        public IActionResult CreateCategory()
        {
            // Kullanıcıya boş formu gösteriyoruz
            return View();
        }

        // ============================
        // 3) CREATE CATEGORY (POST)
        // ============================
        // Formdan gelen veriyi API’ye göndererek yeni kategori eklemek için POST action
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            // HttpClient instance’ı oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // Formdan gelen DTO’yu JSON string’e çeviriyoruz
            var jsonData = JsonConvert.SerializeObject(createCategoryDTO);

            // JSON veriyi request body olarak gönderebilmek için StringContent oluşturuyoruz
            // Encoding.UTF8 -> Türkçe karakterlerde sorun yaşamamak için
            // "application/json" -> API’nin JSON beklediğini belirtmek için
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // API’ye POST isteği atıyoruz (yeni kayıt ekleme)
            var responseMessage = await client.PostAsync(ApiBaseUrl, content);

            // İşlem başarılıysa liste sayfasına dönüyoruz
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryList");
            }

            // Başarısızsa aynı sayfada kalıp formu tekrar gösteriyoruz
            // İstersen buraya ModelState hatası da basabiliriz
            return View(createCategoryDTO);
        }

        // ============================
        // 4) UPDATE CATEGORY (GET)
        // ============================
        // Güncelleme ekranını doldurmak için önce ilgili kategori bilgilerini API’den çekiyoruz
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // API’den ilgili kategoriyi çekiyoruz (GET /api/Categories/{id})
            var responseMessage = await client.GetAsync($"{ApiBaseUrl}/{id}");

            // Eğer API’den başarılı response geldiyse
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON içeriği okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON verisini UpdateCategoryDTO’ya dönüştürüyoruz
                // (Update formu için gerekli alanlar burada olmalı)
                var values = JsonConvert.DeserializeObject<UpdateCategoryDTO>(jsonData);

                // Dolu model ile Update View’a gidiyoruz
                return View(values);
            }

            // Kayıt bulunamadıysa veya hata varsa listeye döndürüyoruz
            return RedirectToAction("CategoryList");
        }

        // ============================
        // 5) UPDATE CATEGORY (POST)
        // ============================
        // Formdan gelen güncel veriyi API’ye PUT ile gönderiyoruz
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDTO updateCategoryDTO)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // Güncelleme DTO'sunu JSON'a çeviriyoruz
            var jsonData = JsonConvert.SerializeObject(updateCategoryDTO);

            // JSON içeriğini request body olarak hazırlıyoruz
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Swagger'da update endpoint'in bu şekilde: PUT /api/Categories/{id}
            // Bu yüzden URL'e CategoryID'yi ekleyerek istek atıyoruz
            var responseMessage = await client.PutAsync($"{ApiBaseUrl}/{updateCategoryDTO.CategoryID}", content);

            // API başarılı dönerse listeye geri yönlendiriyoruz
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("CategoryList");
            }

            // Başarısızsa (404/400/500 vb.) aynı sayfada kalıp formu geri gösteriyoruz
            // İstersen burada hata mesajını da okuyup ekrana basabiliriz
            return View(updateCategoryDTO);
        }


        // ============================
        // 6) DELETE CATEGORY (GET)
        // ============================
        // Silme işlemini link üzerinden çağırıyorsun: /Category/DeleteCategory/{id}
        // Bu metot API’ye DELETE isteği atar
        [HttpGet]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // API’ye DELETE isteği atıyoruz (DELETE /api/Categories/{id})
            var responseMessage = await client.DeleteAsync($"{ApiBaseUrl}/{id}");

            // Başarılı olsun olmasın kullanıcıyı listeye geri döndürüyoruz
            // (istersen başarısız durumda TempData ile mesaj gösterebiliriz)
            return RedirectToAction("CategoryList");
        }
    }
}
