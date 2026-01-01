using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.ProductDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Mapping.ProductMap
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<Product, ResultProductDTO>().ReverseMap(); //Product ile ResultProductDTO arasında çift yönlü eşleme yapar
            CreateMap<Product, CreateProductDTO>().ReverseMap(); //Product ile CreateProductDTO arasında çift yönlü eşleme yapar
            CreateMap<Product, UpdateProductDTO>().ReverseMap(); //Product ile UpdateProductDTO arasında çift yönlü eşleme yapar
            CreateMap<Product, GetProductByIDDTO>().ReverseMap(); //Product ile GetProductByIDDTO arasında çift yönlü eşleme yapar
            CreateMap<Product, ResultProductWithCategoryDTO>()
    .ForMember(dest => dest.CategoryName,
        opt => opt.MapFrom(src => src.Category != null ? src.Category.CategoryName : "")); // Product ile ResultProductWithCategoryDTO arasında eşleme yapar ve CategoryName alanını doldurur  
        }
    }
}
