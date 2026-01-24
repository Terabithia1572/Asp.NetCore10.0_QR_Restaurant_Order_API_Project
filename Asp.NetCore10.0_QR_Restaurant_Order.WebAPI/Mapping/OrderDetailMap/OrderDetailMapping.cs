using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.OrderDetailDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Mapping.OrderDetailMap
{
    public class OrderDetailMapping:Profile
    {
        public OrderDetailMapping()
        {
            CreateMap<OrderDetail,ResultOrderDetailDTO>().ReverseMap(); //OrderDetail ile ResultOrderDetailDTO arasında çift yönlü eşleme yapar
            CreateMap<OrderDetail,GetOrderDetailByIDDTO>().ReverseMap(); //OrderDetail ile GetOrderDetailByIDDTO arasında çift yönlü eşleme yapar
            CreateMap<OrderDetail,CreateOrderDetailDTO>().ReverseMap(); //OrderDetail ile CreateOrderDetailDTO arasında çift yönlü eşleme yapar
            CreateMap<OrderDetail,UpdateOrderDetailDTO>().ReverseMap(); //OrderDetail ile UpdateOrderDetailDTO arasında çift yönlü eşleme yapar
        }
    }
}
