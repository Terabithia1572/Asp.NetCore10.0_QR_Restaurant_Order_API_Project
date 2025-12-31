using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.AboutDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly IAboutService _aboutService;

        public AboutsController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        [HttpGet]
        public IActionResult AboutList() // Tüm About verilerini listeleyen bir API endpoint'i
        {
            var abouts = _aboutService.TGetListAll(); // IAboutService arayüzündeki TGetListAll metodunu kullanarak tüm About verilerini alır
            return Ok(abouts); // Alınan verileri HTTP 200 OK yanıtı ile döner

        }
        [HttpPost]
        public IActionResult CreateAbout(CreateAboutDTO createAboutDTO)
        // Yeni bir About kaydı oluşturan API endpoint'i
        {
            if (createAboutDTO == null)
            {
                // Client geçersiz / boş veri gönderirse HTTP 400 Bad Request
                return BadRequest("Hakkımda bilgileri boş olamaz.");
            }

            // DTO → Entity dönüşümü
            var about = new About
            {
                AboutTitle = createAboutDTO.AboutTitle,
                AboutDescription = createAboutDTO.AboutDescription,
                AboutImageURL = createAboutDTO.AboutImageURL
            };

            // Service katmanı entity ile çalışır
            _aboutService.TAdd(about);

            // REST standardına uygun: 201 Created
            return CreatedAtAction(
                nameof(GetAboutByID),
                new { id = about.AboutID },
                about
            );
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAbout(int id) // Belirtilen ID'ye sahip About kaydını silen bir API endpoint'i
        {
            var about = _aboutService.TGetByID(id); // IAboutService arayüzündeki TGetByID metodunu kullanarak belirtilen ID'ye sahip About kaydını alır
            if (about == null) // Eğer About kaydı bulunamazsa
            {
                return NotFound("Hakkımda Bulunamadı.."); // HTTP 404 Not Found yanıtı döner
            }
            _aboutService.TDelete(about); // IAboutService arayüzündeki TDelete metodunu kullanarak About kaydını siler
            return Ok("Hakkımda Silme İşlemi Başarılı.."); // İşlemin başarılı olduğunu belirten HTTP 200 OK yanıtı döner
        }
        [HttpPut]
        public IActionResult UpdateAbout(UpdateAboutDTO updateAboutDTO) // Mevcut bir About kaydını güncelleyen bir API endpoint'i
        {
            var about = _aboutService.TGetByID(updateAboutDTO.AboutID); // IAboutService arayüzündeki TGetByID metodunu kullanarak güncellenecek About kaydını alır
            if (about == null) // Eğer About kaydı bulunamazsa
            
                return NotFound("Hakkımda Bulunamadı.."); // HTTP 404 Not Found yanıtı döner
            
            about.AboutTitle = updateAboutDTO.AboutTitle;
            about.AboutDescription = updateAboutDTO.AboutDescription;
            about.AboutImageURL = updateAboutDTO.AboutImageURL;

            _aboutService.TUpdate(about); // IAboutService arayüzündeki TUpdate metodunu kullanarak About kaydını günceller
            return NoContent(); // İşlemin başarılı olduğunu belirten HTTP 204 No Content yanıtı döner
        }
        [HttpGet("GetAboutByID/{id}")]
        public IActionResult GetAboutByID(int id) // Belirtilen ID'ye sahip About kaydını getiren bir API endpoint'i
        {
            var about = _aboutService.TGetByID(id); // IAboutService arayüzündeki TGetByID metodunu kullanarak belirtilen ID'ye sahip About kaydını alır
            if (about == null) // Eğer About kaydı bulunamazsa
            {
                return NotFound("Hakkımda Bulunamadı.."); // HTTP 404 Not Found yanıtı döner
            }
            return Ok(about); // Alınan About kaydını HTTP 200 OK yanıtı ile döner
        }
    }
}