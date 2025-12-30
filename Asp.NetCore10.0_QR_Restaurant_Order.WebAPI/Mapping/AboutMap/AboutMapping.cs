using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.AboutDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Mapping.AboutMap
{
    public class AboutMapping:Profile
    {
        public AboutMapping()
        {
            CreateMap<About, ResultAboutDTO>().ReverseMap(); //About ile ResultAboutDTO arasında çift yönlü eşleme yapar
            CreateMap<About, CreateAboutDTO>().ReverseMap(); //About ile CreateAboutDTO arasında çift yönlü eşleme yapar
            CreateMap<About, UpdateAboutDTO>().ReverseMap(); //About ile UpdateAboutDTO arasında çift yönlü eşleme yapar
            CreateMap<About,GetAboutByIDDTO>().ReverseMap(); //About ile GetAboutByIDDTO arasında çift yönlü eşleme yapar
        }
    }
}
