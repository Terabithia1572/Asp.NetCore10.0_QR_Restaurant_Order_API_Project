using Asp.NetCore10._0_QR_Restaurant_Order.WebUI.DTOs.BookingDTOs;
// UI katmanında Booking (Rezervasyon) verilerini taşımak için kullanılan DTO sınıflarını ekliyoruz
// (ResultBookingDTO, CreateBookingDTO, UpdateBookingDTO vb.)

using Microsoft.AspNetCore.Mvc;
// MVC Controller, IActionResult, View, RedirectToAction gibi yapılar için gerekli

using Newtonsoft.Json;
// API’den gelen JSON verisini C# nesnelerine dönüştürmek (Deserialize) için kullanılıyor

using System.Text;
// POST/PUT isteklerinde JSON body gönderebilmek için (StringContent) gerekli

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebUI.Controllers
{
    // Booking (Rezervasyon) işlemlerini yöneten MVC Controller
    // Bu controller API’den veri alır ve Razor View’lara gönderir
    public class BookingController : Controller
    {
        // IHttpClientFactory, HttpClient nesnelerini güvenli ve performanslı şekilde
        // oluşturmak için Dependency Injection ile kullanılan modern yaklaşımdır
        private readonly IHttpClientFactory _httpClientFactory;

        // API base adresini tek yerde tutmak ileride değiştirmeyi kolaylaştırır
        private const string ApiBaseUrl = "https://localhost:7074/api/Bookings";

        // Constructor üzerinden IHttpClientFactory inject edilir
        public BookingController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // ============================
        // 1) BOOKING LIST (GET)
        // ============================
        // Rezervasyon kayıtlarını listelemek için API’ye GET isteği atar
        public async Task<IActionResult> BookingList()
        {
            // IHttpClientFactory üzerinden bir HttpClient instance’ı oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // WebAPI projesindeki Bookings endpoint’ine GET isteği atıyoruz
            var responseMessage = await client.GetAsync(ApiBaseUrl);

            // API başarılı response döndürdüyse devam ediyoruz
            if (responseMessage.IsSuccessStatusCode)
            {
                // Response içeriğini JSON string olarak okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON verisini ResultBookingDTO listesine deserialize ediyoruz
                // Böylece View tarafında güçlü tip (strongly typed) model kullanabiliyoruz
                var values = JsonConvert.DeserializeObject<List<ResultBookingDTO>>(jsonData);

                // DTO listesini View’a gönderiyoruz
                return View(values);
            }

            // API çağrısı başarısız olursa boş liste döndürmek daha güvenli olur
            return View(new List<ResultBookingDTO>());
        }

        // ============================
        // 2) CREATE BOOKING (GET)
        // ============================
        // Yeni rezervasyon ekleme formunu ekrana basar
        [HttpGet]
        public IActionResult CreateBooking()
        {
            // Kullanıcıya boş formu gösteriyoruz
            return View();
        }

        // ============================
        // 3) CREATE BOOKING (POST)
        // ============================
        // Formdan gelen veriyi API’ye göndererek yeni rezervasyon kaydı ekler
        [HttpPost]
        public async Task<IActionResult> CreateBooking(CreateBookingDTO createBookingDTO)
        {
            // HttpClient instance’ı oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // Formdan gelen DTO’yu JSON string’e çeviriyoruz
            var jsonData = JsonConvert.SerializeObject(createBookingDTO);

            // JSON veriyi request body olarak gönderebilmek için StringContent oluşturuyoruz
            // Encoding.UTF8 -> Türkçe karakterlerde sorun yaşamamak için
            // "application/json" -> API’nin JSON beklediğini belirtmek için
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // API’ye POST isteği atıyoruz (yeni kayıt ekleme)
            var responseMessage = await client.PostAsync(ApiBaseUrl, content);

            // İşlem başarılıysa liste sayfasına dönüyoruz
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("BookingList");
            }

            // Başarısızsa aynı sayfada kalıp formu tekrar gösteriyoruz
            return View(createBookingDTO);
        }

        // ============================
        // 4) UPDATE BOOKING (GET)
        // ============================
        // Güncelleme ekranını doldurmak için ilgili rezervasyonu API’den çeker
        [HttpGet]
        public async Task<IActionResult> UpdateBooking(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // API’den ilgili rezervasyonu çekiyoruz (GET /api/Bookings/{id})
            var responseMessage = await client.GetAsync($"{ApiBaseUrl}/{id}");

            // Eğer API’den başarılı response geldiyse
            if (responseMessage.IsSuccessStatusCode)
            {
                // JSON içeriği okuyoruz
                var jsonData = await responseMessage.Content.ReadAsStringAsync();

                // JSON verisini UpdateBookingDTO’ya dönüştürüyoruz
                // (Update formu için gerekli alanlar burada olmalı)
                var values = JsonConvert.DeserializeObject<UpdateBookingDTO>(jsonData);

                // Dolu model ile Update View’a gidiyoruz
                return View(values);
            }

            // Kayıt bulunamadıysa veya hata varsa listeye döndürüyoruz
            return RedirectToAction("BookingList");
        }

        // ============================
        // 5) UPDATE BOOKING (POST)
        // ============================
        // Formdan gelen güncel veriyi API’ye PUT ile gönderiyoruz
        [HttpPost]
        public async Task<IActionResult> UpdateBooking(UpdateBookingDTO updateBookingDTO)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // Güncelleme DTO'sunu JSON'a çeviriyoruz
            var jsonData = JsonConvert.SerializeObject(updateBookingDTO);

            // JSON içeriğini request body olarak hazırlıyoruz
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            // Swagger'da update endpoint'in genelde bu şekilde olur: PUT /api/Bookings/{id}
            // Bu yüzden URL'e BookingID'yi ekleyerek istek atıyoruz
            var responseMessage =
                await client.PutAsync($"{ApiBaseUrl}/{updateBookingDTO.BookingID}", content);

            // API başarılı dönerse listeye geri yönlendiriyoruz
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("BookingList");
            }

            // Başarısızsa aynı sayfada kalıp formu geri gösteriyoruz
            return View(updateBookingDTO);
        }

        // ============================
        // 6) DELETE BOOKING (GET)
        // ============================
        // Silme işlemini link üzerinden çağırıyorsun: /Booking/DeleteBooking/{id}
        // Bu metot API’ye DELETE isteği atar
        [HttpGet]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            // HttpClient oluşturuyoruz
            var client = _httpClientFactory.CreateClient();

            // API’ye DELETE isteği atıyoruz (DELETE /api/Bookings/{id})
            await client.DeleteAsync($"{ApiBaseUrl}/{id}");

            // Silme sonrası listeye geri dönüyoruz
            return RedirectToAction("BookingList");
        }
        // ============================
        // 7) APPROVE BOOKING (GET)
        // ============================
        // Rezervasyonu tek tıkla "Aktif" yapmak için
        [HttpGet]
        public async Task<IActionResult> ApproveBooking(int id)
        {
            await UpdateBookingStatus(id, true);
            return RedirectToAction("BookingList");
        }

        // ============================
        // 8) CANCEL BOOKING (GET)
        // ============================
        // Rezervasyonu tek tıkla "Pasif" yapmak için
        [HttpGet]
        public async Task<IActionResult> CancelBooking(int id)
        {
            await UpdateBookingStatus(id, false);
            return RedirectToAction("BookingList");
        }

        // =====================================================
        // Helper Method: Status Update
        // =====================================================
        // 1) API’den rezervasyonu çek
        // 2) BookingStatus'u güncelle
        // 3) PUT ile API’ye geri gönder
        private async Task UpdateBookingStatus(int id, bool status)
        {
            var client = _httpClientFactory.CreateClient();

            // 1) Rezervasyonu çekiyoruz
            var getResponse = await client.GetAsync($"{ApiBaseUrl}/{id}");
            if (!getResponse.IsSuccessStatusCode) return;

            var jsonData = await getResponse.Content.ReadAsStringAsync();

            // 2) UpdateBookingDTO’ya deserialize ediyoruz (PUT için en uygun DTO bu)
            var booking = JsonConvert.DeserializeObject<UpdateBookingDTO>(jsonData);
            if (booking == null) return;

            // 3) Status’u güncelliyoruz
            booking.BookingStatus = status;

            // 4) PUT ile API’ye gönderiyoruz
            var putJson = JsonConvert.SerializeObject(booking);
            var content = new StringContent(putJson, Encoding.UTF8, "application/json");
            await client.PutAsync($"{ApiBaseUrl}/{booking.BookingID}", content);
        }

    }
}
