using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.AboutDTOs;
// UI katmanında About işlemleri için kullanılan DTO’ları ekliyoruz
// (ResultAboutDTO, CreateAboutDTO, UpdateAboutDTO vb.)

using Microsoft.AspNetCore.Mvc;
// MVC Controller, IActionResult, View, RedirectToAction gibi yapılar için gerekli

using Newtonsoft.Json;
// API’den gelen JSON verilerini C# nesnelerine dönüştürmek için (Deserialize)

using System.Text;
// POST ve PUT işlemlerinde JSON body göndermek için (StringContent)

using System.IO;
// Dosya işlemleri (Path, FileStream) için gerekli

using Microsoft.AspNetCore.Hosting;
// WebRootPath (wwwroot) yolunu almak için gerekli


namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Controllers
{
    // About işlemlerini yöneten MVC Controller
    // Bu controller API ile haberleşir, View’lara veri gönderir
    public class AboutController : Controller
    {
        // IHttpClientFactory:
        // HttpClient nesnelerini güvenli, performanslı ve merkezi şekilde üretmek için kullanılır
        private readonly IHttpClientFactory _httpClientFactory;

        // wwwroot klasör yolunu alabilmek için IWebHostEnvironment kullanıyoruz
        private readonly IWebHostEnvironment _env;

        // About API base adresi
        // Tek noktadan yönetim sağlar (ileride URL değişirse tek yerden düzeltirsin)
        private const string ApiBaseUrl = "https://localhost:7074/api/Abouts";

        // Görselleri kaydedeceğimiz klasör adı (wwwroot/aboutImages)
        // Product tarafında productImages idi, burada aboutImages kullanıyoruz
        private const string AboutImagesFolderName = "aboutImages";

        // Constructor – Dependency Injection
        // IHttpClientFactory + IWebHostEnvironment framework tarafından otomatik inject edilir
        public AboutController(IHttpClientFactory httpClientFactory, IWebHostEnvironment env)
        {
            _httpClientFactory = httpClientFactory;
            _env = env;
        }

        // =====================================================
        // ABOUT LIST (GET)
        // =====================================================
        // About kayıtlarını listeleyen action
        public async Task<IActionResult> AboutList()
        {
            // HttpClient instance oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // GET: /api/Abouts
            var responseMessage = await client.GetAsync(ApiBaseUrl);

            // API başarılı response döndürdüyse
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON response içeriğini string olarak okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON verisini ResultAboutDTO listesine çeviriyoruz
                var values = JsonConvert.DeserializeObject<List<ResultAboutDTO>>(jsonData);

                // DTO listesini View’a gönderiyoruz
                return View(values);
            }

            // API başarısızsa boş liste ile sayfa açılır (hata fırlamaz)
            return View(new List<ResultAboutDTO>());
        }

        // =====================================================
        // CREATE ABOUT (GET)
        // =====================================================
        // Yeni About ekleme formunu ekrana basar
        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        // =====================================================
        // CREATE ABOUT (POST)
        // =====================================================
        // Formdan gelen About verisini API’ye göndererek kaydeder
        [HttpPost]
        public async Task<IActionResult> CreateAbout(CreateAboutDTO createAboutDTO)
        {
            // Basit doğrulama: (istersen kaldırabilirsin)
            if (!ModelState.IsValid)
            {
                return View(createAboutDTO);
            }

            // Eğer dosya seçildiyse wwwroot/aboutImages içine kaydedeceğiz
            if (createAboutDTO.AboutImageFile != null && createAboutDTO.AboutImageFile.Length > 0)
            {
                // wwwroot fiziksel yolu (örn: ...\WebUI\wwwroot)
                var webRootPath = _env.WebRootPath;

                // klasör: wwwroot/aboutImages
                var uploadFolder = Path.Combine(webRootPath, AboutImagesFolderName);

                // Klasör yoksa oluştur
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Dosya uzantısını al (jpg/png/webp/jpeg)
                var extension = Path.GetExtension(createAboutDTO.AboutImageFile.FileName).ToLowerInvariant();

                // Güvenlik: sadece izin verilen uzantılar
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", "Lütfen yalnızca JPG, JPEG, PNG veya WEBP formatında görsel yükleyin.");
                    return View(createAboutDTO);
                }

                // Güvenli benzersiz dosya adı
                var fileName = $"{Guid.NewGuid()}{extension}";

                // Fiziksel dosya yolu
                var filePath = Path.Combine(uploadFolder, fileName);

                // Dosyayı diske yaz
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createAboutDTO.AboutImageFile.CopyToAsync(stream);
                }

                // DB'ye kaydedilecek URL/path (API'ye bunu yolluyoruz)
                createAboutDTO.AboutImageURL = $"/{AboutImagesFolderName}/{fileName}";
            }
            else
            {
                // Görsel zorunlu olsun istersen:
                // ModelState.AddModelError("", "Hakkımızda görseli seçmelisin.");
                // return View(createAboutDTO);
            }

            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // API'ye giden DTO içinde AboutImageURL artık /aboutImages/... olacak
            // Not: IFormFile (AboutImageFile) JSON'a çevrilmez; biz string olan AboutImageURL'yi gönderiyoruz
            var jsonData = JsonConvert.SerializeObject(createAboutDTO);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // POST: /api/Abouts
            var responseMessage = await client.PostAsync(ApiBaseUrl, content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AboutList");
            }

            // Başarısızsa formu tekrar gösteriyoruz
            return View(createAboutDTO);
        }

        // =====================================================
        // UPDATE ABOUT (GET)
        // =====================================================
        // Güncelleme ekranını doldurmak için ilgili About kaydını API’den çeker
        [HttpGet]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // GET: /api/Abouts/{id}
            var responseMessage = await client.GetAsync($"{ApiBaseUrl}/{id}");

            // API başarılıysa
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON veriyi okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON → UpdateAboutDTO
                var values = JsonConvert.DeserializeObject<UpdateAboutDTO>(jsonData);

                // Dolu model ile Update View’a gidiyoruz
                return View(values);
            }

            // Kayıt bulunamazsa listeye dön
            return RedirectToAction("AboutList");
        }

        // =====================================================
        // UPDATE ABOUT (POST)
        // =====================================================
        // Güncellenmiş About kaydını API’ye gönderir
        [HttpPost]
        public async Task<IActionResult> UpdateAbout(UpdateAboutDTO updateAboutDTO)
        {
            // Kullanıcı yeni görsel seçtiyse:
            // 1) Eski görseli sil
            // 2) Yeni görseli wwwroot/aboutImages içine kaydet
            // 3) AboutImageURL'yi yeni path ile güncelle
            if (updateAboutDTO.AboutImageFile != null && updateAboutDTO.AboutImageFile.Length > 0)
            {
                // wwwroot fiziksel yolu
                var webRootPath = _env.WebRootPath;

                // aboutImages klasörü
                var uploadFolder = Path.Combine(webRootPath, AboutImagesFolderName);

                // Klasör yoksa oluştur
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // -------------------------------------------------
                // 1) ESKİ GÖRSELİ SİLME
                // -------------------------------------------------
                // updateAboutDTO.AboutImageURL hidden input'tan gelir
                // Örn: /aboutImages/abc.jpg
                if (!string.IsNullOrWhiteSpace(updateAboutDTO.AboutImageURL))
                {
                    // URL'yi fiziksel path'e çeviriyoruz
                    var oldRelativePath = updateAboutDTO.AboutImageURL.TrimStart('/');

                    // Eski dosyanın fiziksel yolu
                    var oldFilePath = Path.Combine(webRootPath, oldRelativePath);

                    // Dosya gerçekten varsa siliyoruz
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }

                // -------------------------------------------------
                // 2) YENİ GÖRSELİ KAYDETME
                // -------------------------------------------------
                // Uzantıyı al
                var extension = Path.GetExtension(updateAboutDTO.AboutImageFile.FileName).ToLowerInvariant();

                // Güvenlik: sadece izin verilen uzantılar
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("", "Lütfen yalnızca JPG, JPEG, PNG veya WEBP formatında görsel yükleyin.");
                    return View(updateAboutDTO);
                }

                // Güvenli benzersiz dosya adı
                var newFileName = $"{Guid.NewGuid()}{extension}";

                // Yeni dosyanın fiziksel kaydetme yolu
                var newFilePath = Path.Combine(uploadFolder, newFileName);

                // Dosyayı diske yaz
                using (var stream = new FileStream(newFilePath, FileMode.Create))
                {
                    await updateAboutDTO.AboutImageFile.CopyToAsync(stream);
                }

                // -------------------------------------------------
                // 3) DB'ye/API'ye gidecek yeni görsel yolu
                // -------------------------------------------------
                updateAboutDTO.AboutImageURL = $"/{AboutImagesFolderName}/{newFileName}";
            }
            // Eğer kullanıcı yeni görsel seçmediyse:
            // AboutImageURL aynen korunur ve eski dosyaya dokunmayız.

            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // DTO’yu JSON string’e çeviriyoruz
            // Not: AboutImageFile JSON'a çevrilmez, biz string olan AboutImageURL'yi gönderiyoruz
            var jsonData = JsonConvert.SerializeObject(updateAboutDTO);

            // JSON body oluşturuyoruz
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // PUT: /api/Abouts/{id}
            var responseMessage =
                await client.PutAsync($"{ApiBaseUrl}/{updateAboutDTO.AboutID}", content);

            // Başarılıysa listeye yönlendir
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("AboutList");
            }

            // Başarısızsa aynı sayfada kal
            return View(updateAboutDTO);
        }

        // =====================================================
        // DELETE ABOUT
        // =====================================================
        // About kaydını siler (link üzerinden çağrılır)
        // İstersen burada aynı zamanda görsel dosyasını da silebiliriz (aşağıda ekledim)
        [HttpGet]
        public async Task<IActionResult> DeleteAbout(int id)
        {
            // 1) Önce ilgili kaydı çekip görsel yolunu alıyoruz (dosyayı da silebilmek için)
            var client = _httpClientFactory.CreateClient();

            var getResponse = await client.GetAsync($"{ApiBaseUrl}/{id}");
            if (getResponse.IsSuccessStatusCode)
            {
                var jsonData = await getResponse.Content.ReadAsStringAsync();
                var about = JsonConvert.DeserializeObject<UpdateAboutDTO>(jsonData);

                // Görsel yolu varsa fiziksel dosyayı da silelim
                if (about != null && !string.IsNullOrWhiteSpace(about.AboutImageURL))
                {
                    var webRootPath = _env.WebRootPath;
                    var oldRelativePath = about.AboutImageURL.TrimStart('/');
                    var oldFilePath = Path.Combine(webRootPath, oldRelativePath);

                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
            }

            // 2) Sonra API’ye DELETE isteği atıyoruz
            // DELETE: /api/Abouts/{id}
            await client.DeleteAsync($"{ApiBaseUrl}/{id}");

            // Silme sonrası listeye dönüyoruz
            return RedirectToAction("AboutList");
        }
    }
}
