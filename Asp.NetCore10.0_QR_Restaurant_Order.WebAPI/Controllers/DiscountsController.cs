using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.DiscountDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;

        public DiscountsController(IDiscountService discountService, IMapper mapper)
        {
            _discountService = discountService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult DiscountList()
        {
            var discounts = _mapper.Map<List<ResultDiscountDTO>>(_discountService.TGetListAll());
            return Ok(discounts);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetDiscountByID(int id)
        {
            var discount = _discountService.TGetByID(id);
            if (discount == null)
                return NotFound("İndirim bilgisi bulunamadı..");

            var dto = _mapper.Map<ResultDiscountDTO>(discount);
            return Ok(dto);
        }

        [HttpPost]
        public IActionResult CreateDiscount(CreateDiscountDTO createDiscountDTO)
        {
            if (createDiscountDTO == null)
                return BadRequest("İndirim bilgileri boş olamaz.");

            var discount = _mapper.Map<Discount>(createDiscountDTO);
            _discountService.TAdd(discount);

            // 201 Created + Location header (GetDiscountByID endpoint’ine yönlendirir)
            return CreatedAtAction(
                nameof(GetDiscountByID),
                new { id = discount.DiscountID },
                _mapper.Map<ResultDiscountDTO>(discount)
            );
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateDiscount(int id, UpdateDiscountDTO updateDiscountDTO)
        {
            if (updateDiscountDTO == null)
                return BadRequest("İndirim bilgileri boş olamaz.");

            var discount = _discountService.TGetByID(id);
            if (discount == null)
                return NotFound("İndirim bilgisi bulunamadı..");

            // DTO -> mevcut entity üstüne yaz
            _mapper.Map(updateDiscountDTO, discount);

            _discountService.TUpdate(discount);
            return NoContent(); // 204
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteDiscount(int id)
        {
            var discount = _discountService.TGetByID(id);
            if (discount == null)
                return NotFound("İndirim bilgisi bulunamadı..");

            _discountService.TDelete(discount);
            return NoContent(); // 204
        }
    }
}
