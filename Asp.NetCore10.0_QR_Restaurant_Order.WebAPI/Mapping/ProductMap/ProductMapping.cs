using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.ProductDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Mapping.ProductMap
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            // ================================
            // PRODUCT LIST (NORMAL)
            // ================================
            // Product entity -> ResultProductDTO
            // API ProductList() endpointi bunu kullanır
            CreateMap<Product, ResultProductDTO>()
                .ReverseMap();

            // ================================
            // PRODUCT LIST (WITH CATEGORY)
            // ================================
            // Product entity -> ResultProductWithCategoryDTO
            // CategoryName alanı Product.Category.CategoryName üzerinden gelir
            CreateMap<Product, ResultProductWithCategoryDTO>()
                .ForMember(dest => dest.CategoryName,
                           opt => opt.MapFrom(src => src.Category.CategoryName))
                .ReverseMap();

            // ================================
            // CREATE PRODUCT
            // ================================
            // CreateProductDTO -> Product
            // FK hatası almamak için CategoryID'nin kesin map edilmesi kritik
            CreateMap<CreateProductDTO, Product>()
                .ForMember(dest => dest.CategoryID,
                           opt => opt.MapFrom(src => src.CategoryID));

            // İstersen tersini de açabilirsin (şart değil)
            CreateMap<Product, CreateProductDTO>()
                .ForMember(dest => dest.CategoryID,
                           opt => opt.MapFrom(src => src.CategoryID));

            // ================================
            // UPDATE PRODUCT
            // ================================
            // UpdateProductDTO -> Product (mevcut entity üzerine map)
            // CategoryID güncellenebilsin diye map şart
            CreateMap<UpdateProductDTO, Product>()
                .ForMember(dest => dest.CategoryID,
                           opt => opt.MapFrom(src => src.CategoryID));

            // Product -> UpdateProductDTO
            // Update sayfasını doldururken CategoryID de gelsin
            CreateMap<Product, UpdateProductDTO>()
                .ForMember(dest => dest.CategoryID,
                           opt => opt.MapFrom(src => src.CategoryID));
        }
    }
}

