using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.TestimonialDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Mapping.TestimonialMap
{
    public class TestimonialMapping:Profile
    {
        public TestimonialMapping()
        {
            CreateMap<Testimonial, ResultTestimonialDTO>().ReverseMap(); //Testimonial ile ResultTestimonialDTO arasında çift yönlü eşleme yapar
            CreateMap<Testimonial, CreateTestimonialDTO>().ReverseMap(); //Testimonial ile CreateTestimonialDTO arasında çift yönlü eşleme yapar
            CreateMap<Testimonial, UpdateTestimonialDTO>().ReverseMap(); //Testimonial ile UpdateTestimonialDTO arasında çift yönlü eşleme yapar
            CreateMap<Testimonial, GetTestimonialByIDDTO>().ReverseMap(); //Testimonial ile GetTestimonialByIDDTO arasında çift yönlü eşleme yapar
        }
    }
}
