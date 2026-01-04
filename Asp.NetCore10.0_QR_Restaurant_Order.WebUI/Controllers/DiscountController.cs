using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.DiscountDTOs;
// UI katmanında Discount (İndirim) verilerini taşımak için kullanılan DTO sınıflarını ekliyoruz
// (ResultDiscountDTO, CreateDiscountDTO, UpdateDiscountDTO vb.)

using Microsoft.AspNetCore.Mvc;
// MVC Controller, IActionResult, View, RedirectToAction gibi yapılar için gerekli

using Newtonsoft.Json;
// API’den gelen JSON verisini C# nesnelerine dönüştürmek (Deserialize) için kullanılıyor

using System.Text;
// POST/PUT isteklerinde JSON body gönderebilmek için (StringContent) gerekli

using System.IO;
// Dosya işlemleri (Path, FileStream) için gerekli

using Microsoft.AspNetCore.Hosting;
// WebRootPath (wwwroot) yolunu almak için gerekli

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Controllers
{
    // Discount (İndirim) işlemlerini yöneten MVC Controller
    // Bu controller API ile haberleşir, View’lara veri gönderir
    public class DiscountController : Controller
    {
        // IHttpClientFactory:
        // HttpClient nesnelerini güvenli, performanslı ve merkezi şekilde üretmek için kullanılır
        private readonly IHttpClientFactory _httpClientFactory;

        // wwwroot klasör yolunu alabilmek için IWebHostEnvironment kullanıyoruz
        private readonly IWebHostEnvironment _env;

        // Discount API base adresi
        private const string ApiBaseUrl = "https://localhost:7074/api/Discounts";

        // Görsellerin kaydedileceği klasör (wwwroot/discountImages)
        private const string DiscountImagesFolderName = "discountImages";

        // Constructor – Dependency Injection
        public DiscountController(IHttpClientFactory httpClientFactory, IWebHostEnvironment env)
        {
            _httpClientFactory = httpClientFactory;
            _env = env;
        }

        // ============================
        // 1) DISCOUNT LIST (GET)
        // ============================
        // İndirim kayıtlarını listelemek için API’ye GET isteği atar
        public async Task<IActionResult> DiscountList()
        {
            // HttpClient instance oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // GET: /api/Discounts
            var responseMessage = await client.GetAsync(ApiBaseUrl);

            // API başarılı response döndürdüyse
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON içeriğini okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON -> ResultDiscountDTO listesi
                var values = JsonConvert.DeserializeObject<List<ResultDiscountDTO>>(jsonData);

                // View’a gönderiyoruz
                return View(values);
            }

            // API başarısızsa boş liste döndür
            return View(new List<ResultDiscountDTO>());
        }

        // ============================
        // 2) CREATE DISCOUNT (GET)
        // ============================
        // Yeni indirim ekleme formunu ekrana basar
        [HttpGet]
        public IActionResult CreateDiscount()
        {
            return View();
        }

        // ============================
        // 3) CREATE DISCOUNT (POST)
        // ============================
        // Formdan gelen veriyi API’ye göndererek yeni indirim kaydı ekler
        [HttpPost]
        public async Task<IActionResult> CreateDiscount(CreateDiscountDTO createDiscountDTO)
        {
            // ModelState kontrolü (isteğe bağlı)
            if (!ModelState.IsValid)
            {
                return View(createDiscountDTO);
            }

            // Kullanıcı görsel seçtiyse wwwroot/discountImages içine kaydediyoruz
            if (createDiscountDTO.DiscountImageFile != null && createDiscountDTO.DiscountImageFile.Length > 0)
            {
                // wwwroot fiziksel yolu
                var webRootPath = _env.WebRootPath;

                // upload klasörü: wwwroot/discountImages
                var uploadFolder = Path.Combine(webRootPath, DiscountImagesFolderName);

                // klasör yoksa oluştur
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // dosya uzantısı al (jpg/png/webp/jpeg)
                var extension = Path.GetExtension(createDiscountDTO.DiscountImageFile.FileName).ToLowerInvariant();

                // sadece izin verilen uzantılar
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", "Lütfen yalnızca JPG, JPEG, PNG veya WEBP formatında görsel yükleyin.");
                    return View(createDiscountDTO);
                }

                // benzersiz dosya adı
                var fileName = $"{Guid.NewGuid()}{extension}";

                // fiziksel yol
                var filePath = Path.Combine(uploadFolder, fileName);

                // dosyayı kaydet
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createDiscountDTO.DiscountImageFile.CopyToAsync(stream);
                }

                // DB’ye kaydedilecek URL
                createDiscountDTO.DiscountImageURL = $"/{DiscountImagesFolderName}/{fileName}";
            }

            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // DTO’yu JSON’a çeviriyoruz (IFormFile JSON'a çevrilmez, DiscountImageURL string gider)
            var jsonData = JsonConvert.SerializeObject(createDiscountDTO);

            // JSON body hazırlıyoruz
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // POST: /api/Discounts
            var responseMessage = await client.PostAsync(ApiBaseUrl, content);

            // başarılıysa listeye dön
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("DiscountList");
            }

            // başarısızsa aynı formu göster
            return View(createDiscountDTO);
        }

        // ============================
        // 4) UPDATE DISCOUNT (GET)
        // ============================
        // Güncelleme ekranını doldurmak için ilgili indirimi API’den çeker
        [HttpGet]
        public async Task<IActionResult> UpdateDiscount(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // GET: /api/Discounts/{id}
            var responseMessage = await client.GetAsync($"{ApiBaseUrl}/{id}");

            // başarılıysa
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON içeriği al
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON -> UpdateDiscountDTO
                var values = JsonConvert.DeserializeObject<UpdateDiscountDTO>(jsonData);

                // Update view’a gönder
                return View(values);
            }

            // kayıt yoksa listeye dön
            return RedirectToAction("DiscountList");
        }

        // ============================
        // 5) UPDATE DISCOUNT (POST)
        // ============================
        // Formdan gelen güncel veriyi API’ye PUT ile gönderiyoruz
        [HttpPost]
        public async Task<IActionResult> UpdateDiscount(UpdateDiscountDTO updateDiscountDTO)
        {
            // Kullanıcı yeni görsel seçtiyse:
            // 1) Eski görseli sil
            // 2) Yeni görseli kaydet
            // 3) DiscountImageURL’yi güncelle
            if (updateDiscountDTO.DiscountImageFile != null && updateDiscountDTO.DiscountImageFile.Length > 0)
            {
                var webRootPath = _env.WebRootPath;
                var uploadFolder = Path.Combine(webRootPath, DiscountImagesFolderName);

                // klasör yoksa oluştur
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // 1) ESKİ GÖRSELİ SİL
                if (!string.IsNullOrWhiteSpace(updateDiscountDTO.DiscountImageURL))
                {
                    var oldRelativePath = updateDiscountDTO.DiscountImageURL.TrimStart('/');
                    var oldFilePath = Path.Combine(webRootPath, oldRelativePath);

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // 2) YENİ GÖRSELİ KAYDET
                var extension = Path.GetExtension(updateDiscountDTO.DiscountImageFile.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", "Lütfen yalnızca JPG, JPEG, PNG veya WEBP formatında görsel yükleyin.");
                    return View(updateDiscountDTO);
                }

                var newFileName = $"{Guid.NewGuid()}{extension}";
                var newFilePath = Path.Combine(uploadFolder, newFileName);

                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await updateDiscountDTO.DiscountImageFile.CopyToAsync(stream);
                }

                // 3) Yeni URL’yi set ediyoruz
                updateDiscountDTO.DiscountImageURL = $"/{DiscountImagesFolderName}/{newFileName}";
            }
            // Yeni görsel seçilmediyse DiscountImageURL aynen korunur

            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // DTO’yu JSON’a çeviriyoruz
            var jsonData = JsonConvert.SerializeObject(updateDiscountDTO);

            // JSON body hazırlıyoruz
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // PUT: /api/Discounts/{id}
            var responseMessage =
                await client.PutAsync($"{ApiBaseUrl}/{updateDiscountDTO.DiscountID}", content);

            // başarılıysa listeye dön
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("DiscountList");
            }

            // başarısızsa sayfada kal
            return View(updateDiscountDTO);
        }

        // ============================
        // 6) DELETE DISCOUNT (GET)
        // ============================
        // Silme işlemini link üzerinden çağırıyorsun: /Discount/DeleteDiscount/{id}
        // Bu metot:
        // 1) Kaydı çekip görsel yolunu alır
        // 2) Görseli diskten siler
        // 3) API’ye DELETE atar
        [HttpGet]
        public async Task<IActionResult> DeleteDiscount(int id)
        {
            var client = _httpClientFactory.CreateClient();

            // 1) Önce kaydı çekip görseli silebilmek için ImageURL’yi alıyoruz
            var getResponse = await client.GetAsync($"{ApiBaseUrl}/{id}");
            if (getResponse.IsSuccessStatusCode)
            {
                var jsonData = await getResponse.Content.ReadAsStringAsync();
                var discount = JsonConvert.DeserializeObject<UpdateDiscountDTO>(jsonData);

                // Görsel varsa diskten sil
                if (discount != null && !string.IsNullOrWhiteSpace(discount.DiscountImageURL))
                {
                    var webRootPath = _env.WebRootPath;
                    var oldRelativePath = discount.DiscountImageURL.TrimStart('/');
                    var oldFilePath = Path.Combine(webRootPath, oldRelativePath);

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
            }

            // 2) Kaydı API üzerinden siliyoruz
            await client.DeleteAsync($"{ApiBaseUrl}/{id}");

            // 3) Listeye dön
            return RedirectToAction("DiscountList");
        }
    }
}
