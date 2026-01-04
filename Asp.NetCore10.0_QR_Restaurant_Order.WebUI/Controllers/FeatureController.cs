using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.FeatureDTOs;
// UI katmanında Feature (Öne Çıkan Özellik) verilerini taşımak için kullanılan DTO sınıflarını ekliyoruz
// (ResultFeatureDTO, CreateFeatureDTO, UpdateFeatureDTO vb.)

using Microsoft.AspNetCore.Mvc;
// MVC Controller, IActionResult, View, RedirectToAction gibi yapılar için gerekli

using Newtonsoft.Json;
// API’den gelen JSON verisini C# nesnelerine dönüştürmek (Deserialize) için kullanılıyor

using System.Text;
// POST/PUT isteklerinde JSON body gönderebilmek için (StringContent) gerekli

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Controllers
{
    // Feature (Özellikler) işlemlerini yöneten MVC Controller
    // Bu controller API’den veri alır ve Razor View’lara gönderir
    public class FeatureController : Controller
    {
        // IHttpClientFactory, HttpClient nesnelerini güvenli ve performanslı şekilde
        // oluşturmak için Dependency Injection ile kullanılan modern yaklaşımdır
        private readonly IHttpClientFactory _httpClientFactory;

        // API base adresini tek yerde tutmak ileride değiştirmeyi kolaylaştırır
        private const string ApiBaseUrl = "https://localhost:7074/api/Features";

        // Constructor üzerinden IHttpClientFactory inject edilir
        public FeatureController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // ============================
        // 1) FEATURE LIST (GET)
        // ============================
        // Özellik kayıtlarını listelemek için API’ye GET isteği atar
        public async Task<IActionResult> FeatureList()
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // API’ye GET isteği atıyoruz (GET /api/Features)
            var responseMessage = await client.GetAsync(ApiBaseUrl);

            // API başarılı dönerse
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON içeriğini okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON -> ResultFeatureDTO listesi
                var values = JsonConvert.DeserializeObject<List<ResultFeatureDTO>>(jsonData);

                // View'a gönderiyoruz
                return View(values);
            }

            // API başarısızsa boş liste döndür
            return View(new List<ResultFeatureDTO>());
        }

        // ============================
        // 2) CREATE FEATURE (GET)
        // ============================
        // Yeni özellik ekleme formunu ekrana basar
        [HttpGet]
        public IActionResult CreateFeature()
        {
            return View();
        }

        // ============================
        // 3) CREATE FEATURE (POST)
        // ============================
        // Formdan gelen veriyi API’ye göndererek yeni özellik kaydı ekler
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDTO createFeatureDTO)
        {
            // Basit validasyon (istersen DataAnnotations ile daha da güçlendirebiliriz)
            if (!ModelState.IsValid)
            {
                return View(createFeatureDTO);
            }

            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // DTO’yu JSON’a çeviriyoruz
            var jsonData = JsonConvert.SerializeObject(createFeatureDTO);

            // JSON body hazırlıyoruz
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // API’ye POST isteği atıyoruz (POST /api/Features)
            var responseMessage = await client.PostAsync(ApiBaseUrl, content);

            // Başarılıysa listeye yönlendir
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("FeatureList");
            }

            // Başarısızsa sayfada kal
            return View(createFeatureDTO);
        }

        // ============================
        // 4) UPDATE FEATURE (GET)
        // ============================
        // Güncelleme ekranını doldurmak için ilgili kaydı API’den çeker
        [HttpGet]
        public async Task<IActionResult> UpdateFeature(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // GET: /api/Features/{id}
            var responseMessage = await client.GetAsync($"{ApiBaseUrl}/{id}");

            // başarılıysa
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON içeriği okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON -> UpdateFeatureDTO
                var values = JsonConvert.DeserializeObject<UpdateFeatureDTO>(jsonData);

                // Update View’a gönderiyoruz
                return View(values);
            }

            // Kayıt bulunamadıysa listeye dön
            return RedirectToAction("FeatureList");
        }

        // ============================
        // 5) UPDATE FEATURE (POST)
        // ============================
        // Formdan gelen güncel veriyi API’ye PUT ile gönderiyoruz
        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDTO updateFeatureDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(updateFeatureDTO);
            }

            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // DTO’yu JSON’a çeviriyoruz
            var jsonData = JsonConvert.SerializeObject(updateFeatureDTO);

            // JSON body hazırlıyoruz
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // PUT: /api/Features/{id}
            var responseMessage = await client.PutAsync($"{ApiBaseUrl}/{updateFeatureDTO.FeatureID}", content);

            // Başarılıysa listeye dön
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("FeatureList");
            }

            // Başarısızsa sayfada kal
            return View(updateFeatureDTO);
        }

        // ============================
        // 6) DELETE FEATURE (GET)
        // ============================
        // Silme işlemini link üzerinden çağırıyorsun: /Feature/DeleteFeature/{id}
        // Bu metot API’ye DELETE isteği atar
        [HttpGet]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // DELETE: /api/Features/{id}
            await client.DeleteAsync($"{ApiBaseUrl}/{id}");

            // İşlem sonrası listeye dön
            return RedirectToAction("FeatureList");
        }
        // ============================
        // 7) ACTIVATE FEATURE (GET)
        // ============================
        // Özelliği tek tıkla "Aktif" yapmak için
        [HttpGet]
        public async Task<IActionResult> ActivateFeature(int id)
        {
            await UpdateFeatureStatus(id, true);
            return RedirectToAction("FeatureList");
        }

        // ============================
        // 8) DEACTIVATE FEATURE (GET)
        // ============================
        // Özelliği tek tıkla "Pasif" yapmak için
        [HttpGet]
        public async Task<IActionResult> DeactivateFeature(int id)
        {
            await UpdateFeatureStatus(id, false);
            return RedirectToAction("FeatureList");
        }

        // =====================================================
        // Helper Method: Status Update
        // =====================================================
        // 1) API’den feature kaydını çek
        // 2) FeatureStatus’u güncelle
        // 3) PUT ile API’ye geri gönder
        private async Task UpdateFeatureStatus(int id, bool status)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // 1) İlgili kaydı API’den çekiyoruz
            var getResponse = await client.GetAsync($"{ApiBaseUrl}/{id}");
            if (!getResponse.IsSuccessStatusCode) return;

            // JSON içeriği okuyoruz
            var jsonData = await getResponse.Content.ReadAsStringAsync();

            // 2) UpdateFeatureDTO’ya deserialize ediyoruz (PUT için ideal)
            var feature = JsonConvert.DeserializeObject<UpdateFeatureDTO>(jsonData);
            if (feature == null) return;

            // 3) Status’u set ediyoruz
            feature.FeatureStatus = status;

            // 4) PUT ile API’ye geri gönderiyoruz
            var putJson = JsonConvert.SerializeObject(feature);
            var content = new StringContent(putJson, Encoding.UTF8, "application/json");
            await client.PutAsync($"{ApiBaseUrl}/{feature.FeatureID}", content);
        }

    }
}
