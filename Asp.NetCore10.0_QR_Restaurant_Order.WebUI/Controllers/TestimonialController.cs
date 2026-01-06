using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.TestimonialDTOs;
// UI katmanında Testimonial (Müşteri Yorumları) verilerini taşımak için kullanılan DTO sınıflarını ekliyoruz
// (ResultTestimonialDTO, CreateTestimonialDTO, UpdateTestimonialDTO vb.)

using Microsoft.AspNetCore.Mvc;
// MVC Controller, IActionResult, View, RedirectToAction gibi yapılar için gerekli

using Newtonsoft.Json;
// API’den gelen JSON verisini C# nesnelerine dönüştürmek (Deserialize) için kullanılıyor

using System.Text;
// POST/PUT isteklerinde JSON body gönderebilmek için (StringContent) gerekli

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Controllers
{
    // Testimonial (Müşteri Yorumları) işlemlerini yöneten MVC Controller
    // Bu controller API’den veri alır ve Razor View’lara gönderir
    public class TestimonialController : Controller
    {
        // IHttpClientFactory, HttpClient nesnelerini güvenli ve performanslı şekilde
        // oluşturmak için Dependency Injection ile kullanılan modern yaklaşımdır
        private readonly IHttpClientFactory _httpClientFactory;

        // wwwroot’a görsel kaydedeceğimiz için environment bilgisi gerekir
        private readonly IWebHostEnvironment _env;

        // API base adresini tek yerde tutmak ileride değiştirmeyi kolaylaştırır
        private const string ApiBaseUrl = "https://localhost:7074/api/Testimonials";

        // Görselleri kaydedeceğimiz klasör (wwwroot altında)
        private const string UploadFolder = "TestimonialImages";

        // Constructor üzerinden bağımlılıkları inject ediyoruz
        public TestimonialController(IHttpClientFactory httpClientFactory, IWebHostEnvironment env)
        {
            _httpClientFactory = httpClientFactory;
            _env = env;
        }

        // =====================================================
        // 1) TESTIMONIAL LIST (GET)
        // =====================================================
        // Yorumları listelemek için API’ye GET isteği atar
        public async Task<IActionResult> TestimonialList()
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // API’ye GET isteği atıyoruz (GET /api/Testimonials)
            var responseMessage = await client.GetAsync(ApiBaseUrl);

            // API başarılı dönerse
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON içeriği okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON -> ResultTestimonialDTO listesi
                var values = JsonConvert.DeserializeObject<List<ResultTestimonialDTO>>(jsonData);

                // View’a gönderiyoruz
                return View(values);
            }

            // API başarısızsa boş liste döndür
            return View(new List<ResultTestimonialDTO>());
        }

        // =====================================================
        // 2) CREATE TESTIMONIAL (GET)
        // =====================================================
        // Yeni yorum ekleme formunu ekrana basar
        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            return View();
        }

        // =====================================================
        // 3) CREATE TESTIMONIAL (POST)
        // =====================================================
        // Formdan gelen veriyi API’ye göndererek yeni yorum kaydı ekler
        [HttpPost]
        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDTO createTestimonialDTO)
        {
            // Model validasyonu (istersen DataAnnotations ile güçlendiririz)
            if (!ModelState.IsValid)
            {
                return View(createTestimonialDTO);
            }

            // 1) Görsel yüklendiyse wwwroot içine kaydedip URL’ini DTO’ya basıyoruz
            if (createTestimonialDTO.TestimonialImageFile != null && createTestimonialDTO.TestimonialImageFile.Length > 0)
            {
                // Resmi kaydet ve geriye public path dön (örnek: /TestimonialImages/abc.webp)
                var imageUrl = await SaveImageAsync(createTestimonialDTO.TestimonialImageFile);

                // API’ye gönderilecek ImageURL alanını dolduruyoruz
                createTestimonialDTO.TestimonialImageURL = imageUrl;
            }

            // 2) API’ye kayıt atıyoruz
            var client = _httpClientFactory.CreateClient();

            // DTO’yu JSON’a çeviriyoruz
            var jsonData = JsonConvert.SerializeObject(createTestimonialDTO);

            // JSON body hazırlıyoruz
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // POST: /api/Testimonials
            var responseMessage = await client.PostAsync(ApiBaseUrl, content);

            // Başarılıysa listeye dön
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("TestimonialList");
            }

            // Başarısızsa sayfada kal
            return View(createTestimonialDTO);
        }

        // =====================================================
        // 4) UPDATE TESTIMONIAL (GET)
        // =====================================================
        // Güncelleme ekranını doldurmak için ilgili kaydı API’den çeker
        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // GET: /api/Testimonials/{id}
            var responseMessage = await client.GetAsync($"{ApiBaseUrl}/{id}");

            // Başarılıysa
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON içeriği okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON -> UpdateTestimonialDTO
                var values = JsonConvert.DeserializeObject<UpdateTestimonialDTO>(jsonData);

                // View’a gönderiyoruz
                return View(values);
            }

            // Kayıt bulunamadıysa listeye dön
            return RedirectToAction("TestimonialList");
        }

        // =====================================================
        // 5) UPDATE TESTIMONIAL (POST)
        // =====================================================
        // Formdan gelen güncel veriyi API’ye PUT ile gönderiyoruz
        [HttpPost]
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDTO updateTestimonialDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(updateTestimonialDTO);
            }

            // 1) Yeni görsel seçildiyse kaydedip ImageURL’i güncelliyoruz
            // NOT: Görsel seçilmediyse mevcut TestimonialImageURL korunur (hidden input ile taşınmalı)
            if (updateTestimonialDTO.TestimonialImageFile != null && updateTestimonialDTO.TestimonialImageFile.Length > 0)
            {
                var imageUrl = await SaveImageAsync(updateTestimonialDTO.TestimonialImageFile);
                updateTestimonialDTO.TestimonialImageURL = imageUrl;
            }

            // 2) API’ye PUT atıyoruz
            var client = _httpClientFactory.CreateClient();

            var jsonData = JsonConvert.SerializeObject(updateTestimonialDTO);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // PUT: /api/Testimonials/{id}
            var responseMessage = await client.PutAsync($"{ApiBaseUrl}/{updateTestimonialDTO.TestimonialID}", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("TestimonialList");
            }

            return View(updateTestimonialDTO);
        }

        // =====================================================
        // 6) DELETE TESTIMONIAL (GET)
        // =====================================================
        // Silme işlemini link üzerinden çağırıyorsun: /Testimonial/DeleteTestimonial/{id}
        // Bu metot API’ye DELETE isteği atar
        [HttpGet]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // DELETE: /api/Testimonials/{id}
            await client.DeleteAsync($"{ApiBaseUrl}/{id}");

            // Listeye dön
            return RedirectToAction("TestimonialList");
        }

        // =====================================================
        // Helper: Görsel Kaydetme (wwwroot/TestimonialImages)
        // =====================================================
        // - Dosyayı benzersiz isimle kaydeder
        // - Geriye web’de kullanılacak path döner: /TestimonialImages/xxx.jpg
        private async Task<string> SaveImageAsync(IFormFile file)
        {
            // Klasör yolunu oluşturuyoruz: wwwroot/TestimonialImages
            var uploadPath = Path.Combine(_env.WebRootPath, UploadFolder);

            // Klasör yoksa oluştur
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            // Uzantıyı al (jpg/png/webp vb.)
            var extension = Path.GetExtension(file.FileName);

            // Benzersiz dosya adı üret
            var fileName = $"{Guid.NewGuid()}{extension}";

            // Fiziksel kaydetme yolu
            var filePath = Path.Combine(uploadPath, fileName);

            // Dosyayı diske yaz
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Web'de kullanılacak relative path
            return $"/{UploadFolder}/{fileName}";
        }

        // =====================================================
        // (Opsiyonel) Tek tık Aktif/Pasif
        // =====================================================
        // İstersen Footer/Feature gibi Activate/Deactivate endpointlerini de ekleriz.
    }
}
