using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.ContactDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;

        public ContactsController(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult ContactList() // Tüm Contact verilerini listeleyen bir API endpoint'i
        {
            var contacts = _mapper.Map<List<ResultContactDTO>>(_contactService.TGetListAll()); // IContactService arayüzündeki TGetListAll metodunu kullanarak tüm Contact verilerini alır
            return Ok(contacts); // Alınan verileri HTTP 200 OK yanıtı ile döner
        }
        [HttpPost]
        public IActionResult CreateContact(CreateContactDTO createContactDTO)
        // Yeni bir Contact kaydı oluşturan API endpoint'i
        {
            if (createContactDTO == null)
            {
                // Client geçersiz / boş veri gönderirse HTTP 400 Bad Request
                return BadRequest("İletişim bilgileri boş olamaz.");
            }
            // DTO → Entity dönüşümü
            var contact = _mapper.Map<Contact>(createContactDTO);
            // Service katmanı entity ile çalışır
            _contactService.TAdd(contact);
            // REST standardına uygun: 201 Created
            return CreatedAtAction(
                nameof(GetContactByID),
                new { id = contact.ContactID },
                _mapper.Map<ResultContactDTO>(contact)
            );
        }
        [HttpDelete("{id}")] // Belirtilen ID'ye sahip Contact kaydını silen bir API endpoint'i
        public IActionResult DeleteContact(int id)
        {
            var contact = _contactService.TGetByID(id); // IContactService arayüzündeki TGetByID metodunu kullanarak belirtilen ID'ye sahip Contact kaydını alır
            if (contact == null) // Eğer Contact kaydı bulunamazsa
            {
                return NotFound("İletişim Bilgisi Bulunamadı.."); // HTTP 404 Not Found döner
            }

            _contactService.TDelete(contact); // Contact kaydını siler
            return NoContent(); // HTTP 204 No Content döner
        }
        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, UpdateContactDTO updateContactDTO)
        {
            if (updateContactDTO == null) // Client geçersiz / boş veri gönderirse
                return BadRequest("İletişim bilgileri boş olamaz."); // HTTP 400 Bad Request döner
            var contact = _contactService.TGetByID(id); // Güncellenmek istenen Contact kaydını alır
            if (contact == null) // Eğer Contact kaydı bulunamazsa
                return NotFound("İletişim Bilgisi Bulunamadı.."); // HTTP 404 Not Found döner
            if (updateContactDTO.ContactID != 0 && updateContactDTO.ContactID != id)
                return BadRequest("Route id ile DTO id uyuşmuyor.");

            _mapper.Map(updateContactDTO, contact); // DTO → Entity dönüşümü
            _contactService.TUpdate(contact); // Contact kaydını günceller
            return NoContent(); // HTTP 204 No Content döner
        }
        [HttpGet("{id}")]
        public IActionResult GetContactByID(int id)
        {
            var contact = _contactService.TGetByID(id); // Belirtilen ID'ye sahip Contact kaydını alır
            if (contact == null) // Eğer Contact kaydı bulunamazsa
                return NotFound("İletişim Bilgisi Bulunamadı.."); // HTTP 404 Not Found döner
            var contactDTO = _mapper.Map<ResultContactDTO>(contact); // Entity → DTO dönüşümü
            return Ok(contactDTO); // Alınan verileri HTTP 200 OK yanıtı ile döner
        }
    }
}
