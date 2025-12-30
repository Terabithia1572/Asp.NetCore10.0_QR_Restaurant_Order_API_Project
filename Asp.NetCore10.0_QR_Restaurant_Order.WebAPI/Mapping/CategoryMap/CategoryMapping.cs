using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.CategoryDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Mapping.CategoryMap
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<Category,ResultCategoryDTO>().ReverseMap(); //Category ile ResultCategoryDTO arasında çift yönlü eşleme yapar
            CreateMap<Category,CreateCategoryDTO>().ReverseMap(); //Category ile CreateCategoryDTO arasında çift yönlü eşleme yapar
            CreateMap<Category,UpdateCategoryDTO>().ReverseMap(); //Category ile UpdateCategoryDTO arasında çift yönlü eşleme yapar
            CreateMap<Category,GetCategoryByIDDTO>().ReverseMap(); //Category ile GetCategoryByIDDTO arasında çift yönlü eşleme yapar
        }
    }
}
