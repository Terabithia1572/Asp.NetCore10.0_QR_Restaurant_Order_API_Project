using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.ContactDTOs;
// UI katmanında Contact (İletişim) verilerini taşımak için kullanılan DTO sınıflarını ekliyoruz
// (ResultContactDTO, CreateContactDTO, UpdateContactDTO vb.)

using Microsoft.AspNetCore.Mvc;
// MVC Controller, IActionResult, View, RedirectToAction gibi yapılar için gerekli

using Newtonsoft.Json;
// API’den gelen JSON verisini C# nesnelerine dönüştürmek (Deserialize) için kullanılıyor

using System.Text;
// POST/PUT isteklerinde JSON body gönderebilmek için (StringContent) gerekli

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Controllers
{
    // Contact (İletişim) işlemlerini yöneten MVC Controller
    // Bu controller API’den veri alır ve Razor View’lara gönderir
    public class ContactController : Controller
    {
        // IHttpClientFactory, HttpClient nesnelerini güvenli ve performanslı şekilde
        // oluşturmak için Dependency Injection ile kullanılan modern yaklaşımdır
        private readonly IHttpClientFactory _httpClientFactory;

        // API base adresini tek yerde tutmak ileride değiştirmeyi kolaylaştırır
        private const string ApiBaseUrl = "https://localhost:7074/api/Contacts";

        // Constructor üzerinden IHttpClientFactory inject edilir
        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // ============================
        // 1) CONTACT LIST (GET)
        // ============================
        // İletişim kayıtlarını listelemek için API’ye GET isteği atar
        public async Task<IActionResult> ContactList()
        {
            // HttpClient instance’ı oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // WebAPI projesindeki Contacts endpoint’ine GET isteği atıyoruz
            var responseMessage = await client.GetAsync(ApiBaseUrl);

            // API başarılı response döndürdüyse devam ediyoruz
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON response içeriğini okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON -> ResultContactDTO listesi
                var values = JsonConvert.DeserializeObject<List<ResultContactDTO>>(jsonData);

                // View’a gönderiyoruz
                return View(values);
            }

            // Hata olursa boş liste döndürmek daha sağlıklı
            return View(new List<ResultContactDTO>());
        }

        // ============================
        // 2) CREATE CONTACT (GET)
        // ============================
        // Yeni iletişim kaydı ekleme formunu ekrana basar
        [HttpGet]
        public IActionResult CreateContact()
        {
            return View();
        }

        // ============================
        // 3) CREATE CONTACT (POST)
        // ============================
        // Formdan gelen veriyi API’ye göndererek yeni iletişim kaydı ekler
        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactDTO createContactDTO)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // DTO’yu JSON’a çeviriyoruz
            var jsonData = JsonConvert.SerializeObject(createContactDTO);

            // JSON body hazırlıyoruz
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // API’ye POST isteği atıyoruz (POST /api/Contacts)
            var responseMessage = await client.PostAsync(ApiBaseUrl, content);

            // Başarılıysa listeye dön
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ContactList");
            }

            // Başarısızsa aynı formu geri göster
            return View(createContactDTO);
        }

        // ============================
        // 4) UPDATE CONTACT (GET)
        // ============================
        // Güncelleme ekranını doldurmak için ilgili kaydı API’den çeker
        [HttpGet]
        public async Task<IActionResult> UpdateContact(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // GET: /api/Contacts/{id}
            var responseMessage = await client.GetAsync($"{ApiBaseUrl}/{id}");

            // Başarılıysa
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON içeriği okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON -> UpdateContactDTO
                var values = JsonConvert.DeserializeObject<UpdateContactDTO>(jsonData);

                // Update View’a dolu model gönderiyoruz
                return View(values);
            }

            // Kayıt yoksa listeye dön
            return RedirectToAction("ContactList");
        }

        // ============================
        // 5) UPDATE CONTACT (POST)
        // ============================
        // Formdan gelen güncel veriyi API’ye PUT ile gönderiyoruz
        [HttpPost]
        public async Task<IActionResult> UpdateContact(UpdateContactDTO updateContactDTO)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // Güncelleme DTO’sunu JSON’a çeviriyoruz
            var jsonData = JsonConvert.SerializeObject(updateContactDTO);

            // JSON body hazırlıyoruz
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // PUT: /api/Contacts/{id}
            var responseMessage =
                await client.PutAsync($"{ApiBaseUrl}/{updateContactDTO.ContactID}", content);

            // Başarılıysa listeye dön
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ContactList");
            }

            // Başarısızsa aynı sayfada kal
            return View(updateContactDTO);
        }

        // ============================
        // 6) DELETE CONTACT (GET)
        // ============================
        // Silme işlemini link üzerinden çağırıyorsun: /Contact/DeleteContact/{id}
        // Bu metot API’ye DELETE isteği atar
        [HttpGet]
        public async Task<IActionResult> DeleteContact(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // DELETE: /api/Contacts/{id}
            await client.DeleteAsync($"{ApiBaseUrl}/{id}");

            // Silme sonrası listeye dön
            return RedirectToAction("ContactList");
        }
        // ============================
        // 7) ACTIVATE CONTACT (GET)
        // ============================
        // İletişim kaydını tek tıkla "Aktif" yapmak için
        [HttpGet]
        public async Task<IActionResult> ActivateContact(int id)
        {
            await UpdateContactStatus(id, true);
            return RedirectToAction("ContactList");
        }

        // ============================
        // 8) DEACTIVATE CONTACT (GET)
        // ============================
        // İletişim kaydını tek tıkla "Pasif" yapmak için
        [HttpGet]
        public async Task<IActionResult> DeactivateContact(int id)
        {
            await UpdateContactStatus(id, false);
            return RedirectToAction("ContactList");
        }

        // =====================================================
        // Helper Method: Status Update
        // =====================================================
        // 1) API’den contact kaydını çek
        // 2) ContactStatus'u güncelle
        // 3) PUT ile API’ye geri gönder
        private async Task UpdateContactStatus(int id, bool status)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // 1) İlgili contact kaydını API’den çekiyoruz
            var getResponse = await client.GetAsync($"{ApiBaseUrl}/{id}");
            if (!getResponse.IsSuccessStatusCode) return;

            // JSON içeriği okuyoruz
            var jsonData = await getResponse.Content.ReadAsStringAsync();

            // 2) JSON verisini UpdateContactDTO’ya çeviriyoruz (PUT için ideal DTO)
            var contact = JsonConvert.DeserializeObject<UpdateContactDTO>(jsonData);
            if (contact == null) return;

            // 3) Status’u güncelliyoruz
            contact.ContactStatus = status;

            // 4) PUT ile API’ye geri gönderiyoruz
            var putJson = JsonConvert.SerializeObject(contact);
            var content = new StringContent(putJson, Encoding.UTF8, "application/json");
            await client.PutAsync($"{ApiBaseUrl}/{contact.ContactID}", content);
        }

    }
}
