using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.OrderDTO;
using AutoMapper;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Mapping.OrderMap
{
    public class OrderMapping:Profile
    {
        public OrderMapping()
        {
            CreateMap<EntityLayer.Entites.Order, ResultOrderDTO>().ReverseMap(); // Order ile ResultOrderDTO arasında çift yönlü eşleme yapar
            CreateMap<EntityLayer.Entites.Order, CreateOrderDTO>().ReverseMap(); // Order ile CreateOrderDTO arasında çift yönlü eşleme yapar
            CreateMap<EntityLayer.Entites.Order, UpdateOrderDTO>().ReverseMap(); // Order ile UpdateOrderDTO arasında çift yönlü eşleme yapar
            CreateMap<EntityLayer.Entites.Order, GetOrderByIDDTO>().ReverseMap(); // Order ile GetOrderByIDDTO arasında çift yönlü eşleme yapar
        }
    }
}
