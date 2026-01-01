using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.ProductDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var values = _productService.TGetListAll();
            var result = _mapper.Map<List<ResultProductDTO>>(values);
            return Ok(result);
        }
        [HttpGet("with-category")]
        public IActionResult ProductListWithCategory()
        {
            var values = _productService.TGetProductsWithCategories();
            var result = _mapper.Map<List<ResultProductWithCategoryDTO>>(values);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetProductByID(int id)
        {
            var value = _productService.TGetByID(id);
            if (value == null)
                return NotFound("Ürün bulunamadı..");

            var result = _mapper.Map<ResultProductDTO>(value);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDTO createProductDTO)
        {
            if (createProductDTO == null)
                return BadRequest("Ürün bilgileri boş olamaz.");

            var entity = _mapper.Map<Product>(createProductDTO);

            _productService.TAdd(entity);

            return CreatedAtAction(
                nameof(GetProductByID),
                new { id = entity.ProductID },
                _mapper.Map<ResultProductDTO>(entity)
            );
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateProduct(int id, UpdateProductDTO updateProductDTO)
        {
            if (updateProductDTO == null)
                return BadRequest("Ürün bilgileri boş olamaz.");

            var entity = _productService.TGetByID(id);
            if (entity == null)
                return NotFound("Ürün bulunamadı..");

            _mapper.Map(updateProductDTO, entity);
            _productService.TUpdate(entity);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProduct(int id)
        {
            var entity = _productService.TGetByID(id);
            if (entity == null)
                return NotFound("Ürün bulunamadı..");

            _productService.TDelete(entity);
            return NoContent();
        }
    }
}
