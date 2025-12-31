using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.BookingDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;

        public BookingsController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult BookingList()
        {
            var bookings = _mapper.Map<List<ResultBookingDTO>>(_bookingService.TGetListAll());
            return Ok(bookings);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookingByID(int id)
        {
            var booking = _bookingService.TGetByID(id);
            if (booking == null)
                return NotFound("Rezervasyon Bilgisi Bulunamadı..");

            return Ok(_mapper.Map<ResultBookingDTO>(booking));
        }

        [HttpPost]
        public IActionResult CreateBooking(CreateBookingDTO createBookingDTO)
        {
            if (createBookingDTO == null)
                return BadRequest("Rezervasyon bilgileri boş olamaz.");

            var booking = _mapper.Map<Booking>(createBookingDTO);
            _bookingService.TAdd(booking);

            return CreatedAtAction(
                nameof(GetBookingByID),
                new { id = booking.BookingID },
                _mapper.Map<ResultBookingDTO>(booking)
            );
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBooking(int id, UpdateBookingDTO updateBookingDTO)
        {
            if (updateBookingDTO == null)
                return BadRequest("Rezervasyon bilgileri boş olamaz.");

            var booking = _bookingService.TGetByID(id);
            if (booking == null)
                return NotFound("Rezervasyon Bilgisi Bulunamadı..");

            _mapper.Map(updateBookingDTO, booking);
            _bookingService.TUpdate(booking);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var booking = _bookingService.TGetByID(id);
            if (booking == null)
                return NotFound("Rezervasyon Bilgisi Bulunamadı..");

            _bookingService.TDelete(booking);
            return NoContent();
        }
    }
}
