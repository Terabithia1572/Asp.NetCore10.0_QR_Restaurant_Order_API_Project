using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.TestimonialDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestimonialsController : ControllerBase
    {
        private readonly ITestimonialService _testimonialService;
        private readonly IMapper _mapper;

        public TestimonialsController(ITestimonialService testimonialService, IMapper mapper)
        {
            _testimonialService = testimonialService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult TestimonialList()
        {
            var testimonials = _mapper.Map<List<ResultTestimonialDTO>>(_testimonialService.TGetListAll());
            return Ok(testimonials);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTestimonialByID(int id)
        {
            var testimonial = _testimonialService.TGetByID(id);
            if (testimonial == null)
                return NotFound("Referans bilgisi bulunamadı..");

            return Ok(_mapper.Map<ResultTestimonialDTO>(testimonial));
        }

        [HttpPost]
        public IActionResult CreateTestimonial(CreateTestimonialDTO createTestimonialDTO)
        {
            if (createTestimonialDTO == null)
                return BadRequest("Referans bilgileri boş olamaz.");

            var testimonial = _mapper.Map<Testimonial>(createTestimonialDTO);
            _testimonialService.TAdd(testimonial);

            return CreatedAtAction(
                nameof(GetTestimonialByID),
                new { id = testimonial.TestimonialID },
                _mapper.Map<ResultTestimonialDTO>(testimonial)
            );
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateTestimonial(int id, UpdateTestimonialDTO updateTestimonialDTO)
        {
            if (updateTestimonialDTO == null)
                return BadRequest("Referans bilgileri boş olamaz.");

            var testimonial = _testimonialService.TGetByID(id);
            if (testimonial == null)
                return NotFound("Referans bilgisi bulunamadı..");

            _mapper.Map(updateTestimonialDTO, testimonial);
            _testimonialService.TUpdate(testimonial);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteTestimonial(int id)
        {
            var testimonial = _testimonialService.TGetByID(id);
            if (testimonial == null)
                return NotFound("Referans bilgisi bulunamadı..");

            _testimonialService.TDelete(testimonial);
            return NoContent();
        }
    }
}
