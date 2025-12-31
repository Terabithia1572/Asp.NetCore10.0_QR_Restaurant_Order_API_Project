using Asp.NetCore10._0_QR_Restaurant_Order.BusinessLayer.Abstract;
using Asp.NetCore10._0_QR_Restaurant_Order.DTOLayer.DTOs.FeatureDTO;
using Asp.NetCore10._0_QR_Restaurant_Order.EntityLayer.Entites;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Asp.NetCore10._0_QR_Restaurant_Order.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeatureService _featureService;
        private readonly IMapper _mapper;

        public FeaturesController(IFeatureService featureService, IMapper mapper)
        {
            _featureService = featureService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult FeatureList()
        {
            var values = _featureService.TGetListAll();
            var result = _mapper.Map<List<ResultFeatureDTO>>(values);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetFeatureByID(int id)
        {
            var value = _featureService.TGetByID(id);
            if (value == null)
                return NotFound("Özellik bilgisi bulunamadı..");

            var result = _mapper.Map<ResultFeatureDTO>(value);
            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateFeature(CreateFeatureDTO createFeatureDTO)
        {
            if (createFeatureDTO == null)
                return BadRequest("Özellik bilgileri boş olamaz.");

            var entity = _mapper.Map<Feature>(createFeatureDTO);

            _featureService.TAdd(entity);

            return CreatedAtAction(
                nameof(GetFeatureByID),
                new { id = entity.FeatureID },
                _mapper.Map<ResultFeatureDTO>(entity)
            );
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateFeature(int id, UpdateFeatureDTO updateFeatureDTO)
        {
            if (updateFeatureDTO == null)
                return BadRequest("Özellik bilgileri boş olamaz.");

            var entity = _featureService.TGetByID(id);
            if (entity == null)
                return NotFound("Özellik bilgisi bulunamadı..");

            _mapper.Map(updateFeatureDTO, entity);
            _featureService.TUpdate(entity);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteFeature(int id)
        {
            var entity = _featureService.TGetByID(id);
            if (entity == null)
                return NotFound("Özellik bilgisi bulunamadı..");

            _featureService.TDelete(entity);
            return NoContent();
        }
    }
}
