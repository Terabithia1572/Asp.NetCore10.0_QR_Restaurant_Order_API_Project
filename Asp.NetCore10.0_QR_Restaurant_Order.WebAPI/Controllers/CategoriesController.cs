using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.CategoryDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CategoryList()
        {
            var categories = _mapper.Map<List<ResultCategoryDTO>>(_categoryService.TGetListAll()); // Tüm Category verilerini alır ve DTO'ya dönüştürür
            return Ok(categories); // HTTP 200 OK ile döner
        }
        [HttpPost]
        public IActionResult CreateCategory(CreateCategoryDTO createCategoryDTO)
        {
            if (createCategoryDTO == null)
                return BadRequest("Kategori bilgileri boş olamaz.");

            var category = _mapper.Map<Category>(createCategoryDTO);
            _categoryService.TAdd(category);

            return CreatedAtAction(
                nameof(GetCategoryByID),
                new { id = category.CategoryID },
                _mapper.Map<ResultCategoryDTO>(category) // entity yerine DTO dönmek daha temiz
            );
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.TGetByID(id); // Belirtilen ID'ye sahip Category kaydını alır
            if (category == null) // Kayıt bulunamazsa
            {
                return NotFound("Kategori Bilgisi Bulunamadı.."); // HTTP 404 Not Found döner
            }
            _categoryService.TDelete(category); // Kayıt silinir
            return NoContent(); // HTTP 204 No Content döner
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, UpdateCategoryDTO updateCategoryDTO)
        {
            if (updateCategoryDTO == null)
                return BadRequest("Kategori bilgileri boş olamaz.");

            if (id != updateCategoryDTO.CategoryID)
                return BadRequest("Route id ile DTO id uyuşmuyor.");

            var category = _categoryService.TGetByID(id);
            if (category == null)
                return NotFound("Kategori Bilgisi Bulunamadı..");

            _mapper.Map(updateCategoryDTO, category);
            _categoryService.TUpdate(category);

            return NoContent();
        }


        [HttpGet("{id}")]
        public IActionResult GetCategoryByID(int id)
        {
            var category = _categoryService.TGetByID(id); // Belirtilen ID'ye sahip Category kaydını alır
            if (category == null) // Kayıt bulunamazsa
            {
                return NotFound("Kategori Bilgisi Bulunamadı.."); // HTTP 404 Not Found döner
            }
            var resultCategoryDTO = _mapper.Map<ResultCategoryDTO>(category); // Entity'den DTO'ya dönüştürme
            return Ok(resultCategoryDTO); // HTTP 200 OK ile döner
        }
    }
}