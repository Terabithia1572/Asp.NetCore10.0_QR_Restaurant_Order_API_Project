using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.ProductDTOs;
// UI katmanında ürün işlemleri için kullanılan DTO’ları ekliyoruz
// (ResultProductWithCategoryDTO, CreateProductDTO, UpdateProductDTO vb.)

using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.CategoryDTOs;
// UI katmanında kategori listesini API’den çekip dropdown doldurmak için DTO’ları ekliyoruz
// (ResultCategoryDTO vb.)

using Microsoft.AspNetCore.Mvc;
// MVC Controller, IActionResult, View, RedirectToAction gibi yapılar için gerekli

using Microsoft.AspNetCore.Mvc.Rendering;
// Dropdown için SelectListItem kullanabilmek için gerekli

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
    // Ürün işlemlerini yöneten MVC Controller
    // Bu controller API ile haberleşir, View’lara veri gönderir
    public class ProductController : Controller
    {
        // IHttpClientFactory:
        // HttpClient nesnelerini güvenli, performanslı ve merkezi şekilde üretmek için kullanılır
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IWebHostEnvironment _env;
        // wwwroot klasör yolunu alabilmek için IWebHostEnvironment kullanıyoruz


        // Product API base adresi
        // Tek noktadan yönetim sağlar (ileride URL değişirse tek yerden düzeltirsin)
        private const string ApiBaseUrl = "https://localhost:7074/api/Products";

        // Category API adresi
        // Create/Update ekranında kategori dropdown doldurmak için kullanacağız
        private const string CategoryApiUrl = "https://localhost:7074/api/Categories";

        // Constructor – Dependency Injection
        // IHttpClientFactory framework tarafından otomatik inject edilir
        public ProductController(IHttpClientFactory httpClientFactory, IWebHostEnvironment env)
        {
            _httpClientFactory = httpClientFactory;
            _env = env;
        }

        // =====================================================
        // CATEGORY LIST (FOR DROPDOWN)
        // =====================================================
        // Create/Update sayfalarında CategoryID seçebilmek için
        // Kategorileri API’den çekip ViewBag.CategoryList içine doldurur
        private async Task LoadCategoryList()
        {
            // HttpClient instance oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // Kategori listesini API’den çekiyoruz
            // GET: /api/Categories
            var responseMessage = await client.GetAsync(CategoryApiUrl);

            // API başarılı response döndürdüyse
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON response içeriğini string olarak okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON verisini ResultCategoryDTO listesine çeviriyoruz
                var categories = JsonConvert.DeserializeObject<List<ResultCategoryDTO>>(jsonData) ?? new();

                // Dropdown için SelectListItem listesi oluşturuyoruz
                // İstersen sadece aktif kategorileri gösteriyoruz
                ViewBag.CategoryList = categories
                    .Where(x => x.CategoryStatus == true)
                    .Select(x => new SelectListItem
                    {
                        Text = x.CategoryName,              // ekranda görünen isim
                        Value = x.CategoryID.ToString()     // seçilince gidecek id
                    })
                    .ToList();

                return;
            }

            // API başarısızsa dropdown boş gelsin ama sayfa patlamasın
            ViewBag.CategoryList = new List<SelectListItem>();
        }

        // =====================================================
        // PRODUCT LIST (WITH CATEGORY NAME)
        // =====================================================
        // Ürünleri kategori adlarıyla birlikte listeleyen action
        public async Task<IActionResult> ProductList()
        {
            // HttpClient instance oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // API tarafında Product + Category join yapan endpoint
            // GET: /api/Products/with-category
            var responseMessage = await client.GetAsync($"{ApiBaseUrl}/with-category");

            // API başarılı response döndürdüyse
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON response içeriğini string olarak okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON verisini ResultProductWithCategoryDTO listesine çeviriyoruz
                var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDTO>>(jsonData);

                // DTO listesini View’a gönderiyoruz
                return View(values);
            }

            // API başarısızsa boş liste ile sayfa açılır (hata fırlamaz)
            return View(new List<ResultProductWithCategoryDTO>());
        }

        // =====================================================
        // CREATE PRODUCT (GET)
        // =====================================================
        // Yeni ürün ekleme formunu ekrana basar
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            // Kategori dropdown boş gelmesin diye kategori listesini yüklüyoruz
            await LoadCategoryList();

            // Formu gösteriyoruz
            return View();
        }

        // =====================================================
        // CREATE PRODUCT (POST)
        // =====================================================
        // Formdan gelen ürünü API’ye göndererek kaydeder
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDTO createProductDTO)
        {
            // Dropdown tekrar dolu gelsin diye kategori listesini yüklüyoruz
            await LoadCategoryList();

            // Eğer dosya seçildiyse wwwroot/productImages içine kaydedeceğiz
            if (createProductDTO.ProductImageFile != null && createProductDTO.ProductImageFile.Length > 0)
            {
                // wwwroot yolu: _env.WebRootPath
                // klasör: wwwroot/productImages
                var uploadFolder = Path.Combine(_env.WebRootPath, "productImages");

                // Klasör yoksa oluştur
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Dosya uzantısını al (jpg/png/webp)
                var extension = Path.GetExtension(createProductDTO.ProductImageFile.FileName);

                // Güvenli benzersiz dosya adı
                var fileName = $"{Guid.NewGuid()}{extension}";

                // Fiziksel dosya yolu
                var filePath = Path.Combine(uploadFolder, fileName);

                // Dosyayı diske yaz
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await createProductDTO.ProductImageFile.CopyToAsync(stream);
                }

                // DB'ye kaydedilecek URL/path (API'ye bunu yolluyoruz)
                createProductDTO.ProductImageURL = $"/productImages/{fileName}";
            }

            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // API'ye giden DTO içinde ProductImageURL artık /productImages/... olacak
            var jsonData = JsonConvert.SerializeObject(createProductDTO);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // POST: /api/Products
            var responseMessage = await client.PostAsync(ApiBaseUrl, content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }

            // Başarısızsa formu tekrar gösteriyoruz
            return View(createProductDTO);
        }


        // =====================================================
        // UPDATE PRODUCT (GET)
        // =====================================================
        // Güncelleme ekranını doldurmak için ilgili ürünü API’den çeker
        [HttpGet]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            // Update sayfasında kategori seçimi yapılacağı için dropdown'u dolduruyoruz
            await LoadCategoryList();

            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // API’den ürünü ID’ye göre çekiyoruz
            // GET: /api/Products/{id}
            var responseMessage = await client.GetAsync($"{ApiBaseUrl}/{id}");

            // API başarılıysa
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON veriyi okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON → UpdateProductDTO
                var values = JsonConvert.DeserializeObject<UpdateProductDTO>(jsonData);

                // Dolu model ile Update View’a gidiyoruz
                return View(values);
            }

            // Ürün bulunamazsa listeye dön
            return RedirectToAction("ProductList");
        }

        // =====================================================
        // UPDATE PRODUCT (POST)
        // =====================================================
        // Güncellenmiş ürünü API’ye gönderir
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDTO updateProductDTO)
        {
            // Güncelleme sırasında hata olursa View geri döneceği için
            // dropdown tekrar dolu gelsin diye yüklemeyi burada da yapıyoruz
            await LoadCategoryList();

            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // DTO’yu JSON string’e çeviriyoruz
            var jsonData = JsonConvert.SerializeObject(updateProductDTO);

            // JSON body oluşturuyoruz
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Swagger’da endpoint şu şekilde olduğu için ID’yi route’a ekliyoruz
            // PUT: /api/Products/{id}
            var responseMessage =
                await client.PutAsync($"{ApiBaseUrl}/{updateProductDTO.ProductID}", content);

            // Başarılıysa listeye yönlendir
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ProductList");
            }

            // Başarısızsa aynı sayfada kal
            return View(updateProductDTO);
        }

        // =====================================================
        // DELETE PRODUCT
        // =====================================================
        // Ürünü siler (link üzerinden çağrılır)
        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // API’ye DELETE isteği atıyoruz
            // DELETE: /api/Products/{id}
            await client.DeleteAsync($"{ApiBaseUrl}/{id}");

            // Silme sonrası listeye dönüyoruz
            return RedirectToAction("ProductList");
        }
    }
}
