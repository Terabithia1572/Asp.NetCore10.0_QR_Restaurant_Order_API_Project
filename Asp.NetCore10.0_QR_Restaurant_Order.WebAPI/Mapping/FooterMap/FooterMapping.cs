using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.FooterDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Mapping.FooterMap
{
    public class FooterMapping:Profile
    {
        public FooterMapping()
        {
            CreateMap<Footer, ResultFooterDTO>().ReverseMap(); //Footer ile ResultFooterDTO arasında çift yönlü eşleme yapar
            CreateMap<Footer, CreateFooterDTO>().ReverseMap(); //Footer ile CreateFooterDTO arasında çift yönlü eşleme yapar
            CreateMap<Footer, UpdateFooterDTO>().ReverseMap(); //Footer ile UpdateFooterDTO arasında çift yönlü eşleme yapar
            CreateMap<Footer, GetFooterByIDDTO>().ReverseMap(); //Footer ile GetFooterByIDDTO arasında çift yönlü eşleme yapar
        }
    }
}
