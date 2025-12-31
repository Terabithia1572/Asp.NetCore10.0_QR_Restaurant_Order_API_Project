using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.BookingDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpGet]
        public IActionResult BookingList()
        {
            var bookings = _bookingService.TGetListAll(); // Tüm Booking verilerini alır
            return Ok(bookings); // HTTP 200 OK ile döner
        }
        [HttpPost]
        public IActionResult CreateBooking(CreateBookingDTO createBookingDTO)
        {
            if (createBookingDTO == null) // Gelen DTO null ise
            {
                return BadRequest("Rezervasyon bilgileri boş olamaz."); // HTTP 400 Bad Request döner
            }
            var booking = new Booking // Yeni Booking nesnesi oluşturur
            {
                BookingName = createBookingDTO.BookingName, // Ad
                BookingPhone = createBookingDTO.BookingPhone, // Telefon
                BookingDate = createBookingDTO.BookingDate, // Tarih
                BookingStatus = createBookingDTO.BookingStatus, // Durum
                BookingMail = createBookingDTO.BookingMail, // Mail
                BookingPersonCount = createBookingDTO.BookingPersonCount // Kişi Sayısı
            };
            _bookingService.TAdd(booking); // Veritabanına ekler
            return CreatedAtAction( // HTTP 201 Created döner
                nameof(GetBookingByID), // Yeni oluşturulan kaydı almak için kullanılacak action
                new { id = booking.BookingID }, // Yeni kaydın ID'si
                booking // Yeni oluşturulan Booking nesnesi
            );
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _bookingService.TGetByID(id); // Belirtilen ID'ye sahip Booking kaydını alır
            if (booking == null) // Kayıt bulunamazsa
            {
                return NotFound("Rezervasyon Bilgisi Bulunamadı.."); // HTTP 404 Not Found döner
            }
            _bookingService.TDelete(booking); // Kayıt silinir
            return NoContent(); // HTTP 204 No Content döner
        }
        [HttpPut]
        public IActionResult UpdateBooking(UpdateBookingDTO updateBookingDTO) // Rezervasyon güncelleme
        {
            if (updateBookingDTO == null) // Gelen DTO null ise
            {
                return BadRequest("Rezervasyon bilgileri boş olamaz."); // HTTP 400 Bad Request döner
            }
            var existingBooking = _bookingService.TGetByID(updateBookingDTO.BookingID); // Mevcut rezervasyonu al
            if (existingBooking == null) // Rezervasyon bulunamazsa
            {
                return NotFound("Rezervasyon Bilgisi Bulunamadı.."); // HTTP 404 Not Found döner
            }
            existingBooking.BookingName = updateBookingDTO.BookingName; // Rezervasyon adını güncelle
            existingBooking.BookingPhone = updateBookingDTO.BookingPhone; // Rezervasyon telefonunu güncelle
            existingBooking.BookingDate = updateBookingDTO.BookingDate; // Rezervasyon tarihini güncelle
            existingBooking.BookingStatus = updateBookingDTO.BookingStatus; // Rezervasyon durumunu güncelle
            existingBooking.BookingMail = updateBookingDTO.BookingMail; // Rezervasyon mailini güncelle
            existingBooking.BookingPersonCount = updateBookingDTO.BookingPersonCount; // Rezervasyon kişi sayısını güncelle
            _bookingService.TUpdate(existingBooking); // Rezervasyonu veritabanında güncelle
            return NoContent(); // HTTP 204 No Content döner
        }

        [HttpGet("{id}")]
        public IActionResult GetBookingByID(int id)
        {
            var booking = _bookingService.TGetByID(id); // Belirtilen ID'ye sahip Booking kaydını alır
            if (booking == null) // Kayıt bulunamazsa
            {
                return NotFound("Rezervasyon Bilgisi Bulunamadı.."); // HTTP 404 Not Found döner
            } 
            return Ok(booking); // HTTP 200 OK ile döner
        }
    }
}
