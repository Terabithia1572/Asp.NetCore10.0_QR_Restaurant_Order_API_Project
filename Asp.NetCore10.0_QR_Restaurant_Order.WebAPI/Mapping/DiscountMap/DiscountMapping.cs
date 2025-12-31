using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.DiscountDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Mapping.DiscountMap
{
    public class DiscountMapping:Profile
    {
        public DiscountMapping()
        {
            CreateMap<Discount, ResultDiscountDTO>().ReverseMap(); //Discount ile ResultDiscountDTO arasında çift yönlü eşleme yapar
            CreateMap<Discount, CreateDiscountDTO>().ReverseMap(); //Discount ile CreateDiscountDTO arasında çift yönlü eşleme yapar
            CreateMap<Discount, UpdateDiscountDTO>().ReverseMap(); //Discount ile UpdateDiscountDTO arasında çift yönlü eşleme yapar
            CreateMap<Discount, GetDiscountByIDDTO>().ReverseMap(); //Discount ile GetDiscountByIDDTO arasında çift yönlü eşleme yapar
        }
    }
}
