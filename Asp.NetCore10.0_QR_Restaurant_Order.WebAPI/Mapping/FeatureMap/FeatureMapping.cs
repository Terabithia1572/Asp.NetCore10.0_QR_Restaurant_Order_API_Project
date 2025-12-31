using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.FeatureDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Mapping.FeatureMap
{
    public class FeatureMapping:Profile
    {
        public FeatureMapping()
        {
            CreateMap<Feature, ResultFeatureDTO>().ReverseMap(); //Feature ile ResultFeatureDTO arasında çift yönlü eşleme yapar
            CreateMap<Feature, CreateFeatureDTO>().ReverseMap(); //Feature ile CreateFeatureDTO arasında çift yönlü eşleme yapar
            CreateMap<Feature, UpdateFeatureDTO>().ReverseMap(); //Feature ile UpdateFeatureDTO arasında çift yönlü eşleme yapar
            CreateMap<Feature, GetFeatureByIDDTO>().ReverseMap(); //Feature ile GetFeatureByIDDTO arasında çift yönlü eşleme yapar
        }
    }
}
