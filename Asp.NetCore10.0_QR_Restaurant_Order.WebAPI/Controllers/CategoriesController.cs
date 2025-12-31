using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.CategoryDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;
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
            var categories = _mapper.Map<List<ResultCategoryDTO>>(_categoryService.TGetListAll());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryByID(int id)
        {
            var category = _categoryService.TGetByID(id);
            if (category == null)
                return NotFound("Kategori Bilgisi Bulunamadı..");

            return Ok(_mapper.Map<ResultCategoryDTO>(category));
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
                _mapper.Map<ResultCategoryDTO>(category)
            );
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCategory(int id, UpdateCategoryDTO updateCategoryDTO)
        {
            if (updateCategoryDTO == null)
                return BadRequest("Kategori bilgileri boş olamaz.");

            var category = _categoryService.TGetByID(id);
            if (category == null)
                return NotFound("Kategori Bilgisi Bulunamadı..");

            _mapper.Map(updateCategoryDTO, category);
            _categoryService.TUpdate(category);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            var category = _categoryService.TGetByID(id);
            if (category == null)
                return NotFound("Kategori Bilgisi Bulunamadı..");

            _categoryService.TDelete(category);
            return NoContent();
        }
    }
}
