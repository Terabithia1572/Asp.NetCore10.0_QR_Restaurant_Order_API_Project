using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.FooterDTOs;
// UI katmanında Footer (Alt Bilgi) verilerini taşımak için kullanılan DTO sınıflarını ekliyoruz
// (ResultFooterDTO, CreateFooterDTO, UpdateFooterDTO vb.)

using Microsoft.AspNetCore.Mvc;
// MVC Controller, IActionResult, View, RedirectToAction gibi yapılar için gerekli

using Newtonsoft.Json;
// API’den gelen JSON verisini C# nesnelerine dönüştürmek (Deserialize) için kullanılıyor

using System.Text;
// POST/PUT isteklerinde JSON body gönderebilmek için (StringContent) gerekli

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Controllers
{
    // Footer (Alt Bilgi) işlemlerini yöneten MVC Controller
    // Bu controller API ile haberleşir ve View'lara veri gönderir
    public class FooterController : Controller
    {
        // IHttpClientFactory, HttpClient nesnelerini güvenli ve performanslı şekilde
        // oluşturmak için Dependency Injection ile kullanılan modern yaklaşımdır
        private readonly IHttpClientFactory _httpClientFactory;

        // API base adresini tek yerde tutmak ileride değiştirmeyi kolaylaştırır
        private const string ApiBaseUrl = "https://localhost:7074/api/Footers";

        // Constructor üzerinden IHttpClientFactory inject edilir
        public FooterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // ============================
        // 1) FOOTER LIST (GET)
        // ============================
        // Footer kayıtlarını listelemek için API’ye GET isteği atar
        public async Task<IActionResult> FooterList()
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // API’ye GET isteği atıyoruz (GET /api/Footers)
            var responseMessage = await client.GetAsync(ApiBaseUrl);

            // API başarılı dönerse
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON içeriğini okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON -> ResultFooterDTO listesi
                var values = JsonConvert.DeserializeObject<List<ResultFooterDTO>>(jsonData);

                // View’a gönderiyoruz
                return View(values);
            }

            // API başarısızsa boş liste döndür
            return View(new List<ResultFooterDTO>());
        }

        // ============================
        // 2) CREATE FOOTER (GET)
        // ============================
        // Yeni footer ekleme formunu ekrana basar
        [HttpGet]
        public IActionResult CreateFooter()
        {
            return View();
        }

        // ============================
        // 3) CREATE FOOTER (POST)
        // ============================
        // Formdan gelen veriyi API’ye göndererek yeni footer kaydı ekler
        [HttpPost]
        public async Task<IActionResult> CreateFooter(CreateFooterDTO createFooterDTO)
        {
            // Model validasyonu (isteğe bağlı)
            if (!ModelState.IsValid)
            {
                return View(createFooterDTO);
            }

            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // DTO’yu JSON’a çeviriyoruz
            var jsonData = JsonConvert.SerializeObject(createFooterDTO);

            // JSON body hazırlıyoruz
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // POST: /api/Footers
            var responseMessage = await client.PostAsync(ApiBaseUrl, content);

            // Başarılıysa listeye yönlendir
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("FooterList");
            }

            // Başarısızsa sayfada kal
            return View(createFooterDTO);
        }

        // ============================
        // 4) UPDATE FOOTER (GET)
        // ============================
        // Güncelleme ekranını doldurmak için ilgili kaydı API’den çeker
        [HttpGet]
        public async Task<IActionResult> UpdateFooter(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // GET: /api/Footers/{id}
            var responseMessage = await client.GetAsync($"{ApiBaseUrl}/{id}");

            // Başarılıysa
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON içeriği okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON -> UpdateFooterDTO
                var values = JsonConvert.DeserializeObject<UpdateFooterDTO>(jsonData);

                // View’a gönderiyoruz
                return View(values);
            }

            // Kayıt bulunamadıysa listeye dön
            return RedirectToAction("FooterList");
        }

        // ============================
        // 5) UPDATE FOOTER (POST)
        // ============================
        // Formdan gelen güncel veriyi API’ye PUT ile gönderiyoruz
        [HttpPost]
        public async Task<IActionResult> UpdateFooter(UpdateFooterDTO updateFooterDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(updateFooterDTO);
            }

            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // DTO’yu JSON’a çeviriyoruz
            var jsonData = JsonConvert.SerializeObject(updateFooterDTO);

            // JSON body hazırlıyoruz
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // PUT: /api/Footers/{id}
            var responseMessage = await client.PutAsync($"{ApiBaseUrl}/{updateFooterDTO.FooterID}", content);

            // Başarılıysa listeye dön
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("FooterList");
            }

            // Başarısızsa sayfada kal
            return View(updateFooterDTO);
        }

        // ============================
        // 6) DELETE FOOTER (GET)
        // ============================
        // Silme işlemini link üzerinden çağırıyorsun: /Footer/DeleteFooter/{id}
        // Bu metot API’ye DELETE isteği atar
        [HttpGet]
        public async Task<IActionResult> DeleteFooter(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // DELETE: /api/Footers/{id}
            await client.DeleteAsync($"{ApiBaseUrl}/{id}");

            // Listeye dön
            return RedirectToAction("FooterList");
        }

        // =====================================================
        // (Opsiyonel) Tek tık Aktif/Pasif - İstersen ekleriz
        // =====================================================
        // Footer'da genelde 1 tane "aktif" kayıt mantığı olur.
        // (Şimdilik sadece CRUD bıraktım, istersen Contact/Feature gibi ekleriz.)

        // ============================
        // 7) ACTIVATE FOOTER (GET)
        // ============================
        // Footer kaydını tek tıkla "Aktif" yapmak için
        [HttpGet]
        public async Task<IActionResult> ActivateFooter(int id)
        {
            await UpdateFooterStatus(id, true);
            return RedirectToAction("FooterList");
        }

        // ============================
        // 8) DEACTIVATE FOOTER (GET)
        // ============================
        // Footer kaydını tek tıkla "Pasif" yapmak için
        [HttpGet]
        public async Task<IActionResult> DeactivateFooter(int id)
        {
            await UpdateFooterStatus(id, false);
            return RedirectToAction("FooterList");
        }

        // =====================================================
        // Helper Method: Status Update
        // =====================================================
        // 1) API’den footer kaydını çek
        // 2) FooterStatus’u güncelle
        // 3) PUT ile API’ye geri gönder
        private async Task UpdateFooterStatus(int id, bool status)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // 1) İlgili kaydı API’den çekiyoruz
            var getResponse = await client.GetAsync($"{ApiBaseUrl}/{id}");
            if (!getResponse.IsSuccessStatusCode) return;

            // JSON içeriği okuyoruz
            var jsonData = await getResponse.Content.ReadAsStringAsync();

            // 2) UpdateFooterDTO’ya deserialize ediyoruz (PUT için ideal)
            var footer = JsonConvert.DeserializeObject<UpdateFooterDTO>(jsonData);
            if (footer == null) return;

            // 3) Status'u set ediyoruz
            footer.FooterStatus = status;

            // 4) PUT ile API’ye geri gönderiyoruz
            var putJson = JsonConvert.SerializeObject(footer);
            var content = new StringContent(putJson, Encoding.UTF8, "application/json");
            await client.PutAsync($"{ApiBaseUrl}/{footer.FooterID}", content);
        }

    }
}
