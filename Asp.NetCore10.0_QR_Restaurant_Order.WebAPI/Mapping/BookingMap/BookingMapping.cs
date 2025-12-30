using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.BookingDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Mapping.BookingMap
{
    public class BookingMapping:Profile
    {
        public BookingMapping()
        {
            CreateMap<Booking, ResultBookingDTO>().ReverseMap(); //Booking ile ResultBookingDTO arasında çift yönlü eşleme yapar
            CreateMap<Booking, CreateBookingDTO>().ReverseMap(); //Booking ile CreateBookingDTO arasında çift yönlü eşleme yapar
            CreateMap<Booking, UpdateBookingDTO>().ReverseMap(); //Booking ile UpdateBookingDTO arasında çift yönlü eşleme yapar
            CreateMap<Booking, GetBookingByIDDTO>().ReverseMap(); //Booking ile GetBookingByIDDTO arasında çift yönlü eşleme yapar
        }
    }
}
