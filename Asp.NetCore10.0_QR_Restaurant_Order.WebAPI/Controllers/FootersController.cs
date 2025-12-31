using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.FooterDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FootersController : ControllerBase
    {
        private readonly IFooterService _footerService;
        private readonly IMapper _mapper;

        public FootersController(IFooterService footerService, IMapper mapper)
        {
            _footerService = footerService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult FooterList()
        {
            var footers = _mapper.Map<List<ResultFooterDTO>>(_footerService.TGetListAll());
            return Ok(footers);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetFooterByID(int id)
        {
            var footer = _footerService.TGetByID(id);
            if (footer == null)
                return NotFound("Footer bilgisi bulunamadı..");

            return Ok(_mapper.Map<ResultFooterDTO>(footer));
        }

        [HttpPost]
        public IActionResult CreateFooter(CreateFooterDTO createFooterDTO)
        {
            if (createFooterDTO == null)
                return BadRequest("Footer bilgileri boş olamaz.");

            var footer = _mapper.Map<Footer>(createFooterDTO);
            _footerService.TAdd(footer);

            return CreatedAtAction(
                nameof(GetFooterByID),
                new { id = footer.FooterID },
                _mapper.Map<ResultFooterDTO>(footer)
            );
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateFooter(int id, UpdateFooterDTO updateFooterDTO)
        {
            if (updateFooterDTO == null)
                return BadRequest("Footer bilgileri boş olamaz.");

            var footer = _footerService.TGetByID(id);
            if (footer == null)
                return NotFound("Footer bilgisi bulunamadı..");

            _mapper.Map(updateFooterDTO, footer);
            _footerService.TUpdate(footer);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteFooter(int id)
        {
            var footer = _footerService.TGetByID(id);
            if (footer == null)
                return NotFound("Footer bilgisi bulunamadı..");

            _footerService.TDelete(footer);
            return NoContent();
        }
    }
}
