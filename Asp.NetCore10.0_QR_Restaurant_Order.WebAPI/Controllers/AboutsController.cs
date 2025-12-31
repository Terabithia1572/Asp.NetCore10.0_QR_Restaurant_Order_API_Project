using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.AboutDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutsController : ControllerBase
    {
        private readonly IAboutService _aboutService;
        private readonly IMapper _mapper;

        public AboutsController(IAboutService aboutService, IMapper mapper)
        {
            _aboutService = aboutService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult AboutList()
        {
            var abouts = _mapper.Map<List<ResultAboutDTO>>(_aboutService.TGetListAll());
            return Ok(abouts);
        }

        [HttpGet("{id}")]
        public IActionResult GetAboutByID(int id)
        {
            var about = _aboutService.TGetByID(id);
            if (about == null)
                return NotFound("Hakkımda Bilgisi Bulunamadı..");

            return Ok(_mapper.Map<ResultAboutDTO>(about));
        }

        [HttpPost]
        public IActionResult CreateAbout(CreateAboutDTO createAboutDTO)
        {
            if (createAboutDTO == null)
                return BadRequest("Hakkımda bilgileri boş olamaz.");

            var about = _mapper.Map<About>(createAboutDTO);
            _aboutService.TAdd(about);

            return CreatedAtAction(
                nameof(GetAboutByID),
                new { id = about.AboutID },
                _mapper.Map<ResultAboutDTO>(about)
            );
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAbout(int id, UpdateAboutDTO updateAboutDTO)
        {
            if (updateAboutDTO == null)
                return BadRequest("Hakkımda bilgileri boş olamaz.");

            var about = _aboutService.TGetByID(id);
            if (about == null)
                return NotFound("Hakkımda Bilgisi Bulunamadı..");

            _mapper.Map(updateAboutDTO, about);
            _aboutService.TUpdate(about);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAbout(int id)
        {
            var about = _aboutService.TGetByID(id);
            if (about == null)
                return NotFound("Hakkımda Bilgisi Bulunamadı..");

            _aboutService.TDelete(about);
            return NoContent();
        }
    }
}
