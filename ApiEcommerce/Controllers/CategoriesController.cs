using ApiEcommerce.Constants;
using ApiEcommerce.Models.Dtos;
using ApiEcommerce.Repository;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiEcommerce.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    // [EnableCors("AllowSpecificOrigin")]
    // [EnableCors(PolicyNames.AllowSpecificOrigin)]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
        [MapToApiVersion("1.0")]
        // [EnableCors("AllowSpecificOrigin")]
        // [EnableCors(PolicyNames.AllowSpecificOrigin)]
        public IActionResult GetCategories()
        {
            var categories = _categoryRepository.GetCategories();
            var categoriesDto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoriesDto.Add(_mapper.Map<CategoryDto>(category));
            }
            return Ok(categoriesDto);
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(List<CategoryDto>), StatusCodes.Status200OK)]
        [MapToApiVersion("2.0")]
        // [EnableCors("AllowSpecificOrigin")]
        // [EnableCors(PolicyNames.AllowSpecificOrigin)]
        public IActionResult GetCategoriesOrderById()
        {
            var categories = _categoryRepository.GetCategories().OrderBy(cat=>cat.Id);
            var categoriesDto = new List<CategoryDto>();
            foreach (var category in categories)
            {
                categoriesDto.Add(_mapper.Map<CategoryDto>(category));
            }
            return Ok(categoriesDto);
        }
        

        [HttpGet("{id:int}", Name = "GetCategory")]
        //[ResponseCache(Duration = 10)]
        // [ResponseCache(CacheProfileName = "Default10")]
        [ResponseCache(CacheProfileName = CacheProfiles.Default10)]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]


        public IActionResult GetCategory(int id)
        {
            //System.Console.WriteLine($"Categoría con el ID: {id} a las {DateTime.Now}");
            var category = _categoryRepository.GetCategory(id);
            //System.Console.WriteLine($"Respuesta con el ID: {id}");
            if (category == null)
            {
                return NotFound($"La categoría con el id {id} no existe");
            }
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Ok(categoryDto);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public IActionResult CreateCategory([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            if (createCategoryDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_categoryRepository.CategoryExists(createCategoryDTO.Name))
            {
                ModelState.AddModelError("CustomError", "La categoría ya existe");
                return BadRequest(ModelState);

            }
            var category = _mapper.Map<Category>(createCategoryDTO);
            if (!_categoryRepository.CreateCategory(category))
            {
                ModelState.AddModelError("CustomError", $"Algo salió mal al guardar el registro {category.Name}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetCategory", new { id = category.Id }, category);
        }


        [HttpPatch("{id:int}", Name = "updateCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public IActionResult UpdateCategory(int id, [FromBody] CreateCategoryDTO updateCategoryDTO)
        {

            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound($"La categoría con el id {id} no existe");
            }


            if (updateCategoryDTO == null)
            {
                return BadRequest(ModelState);
            }

            if (_categoryRepository.CategoryExists(updateCategoryDTO.Name))
            {
                ModelState.AddModelError("CustomError", "La categoría ya existe");
                return BadRequest(ModelState);

            }
            var category = _mapper.Map<Category>(updateCategoryDTO);
            category.Id = id;
            if (!_categoryRepository.UpdateCategory(category))
            {
                ModelState.AddModelError("CustomError", $"Algo salió mal al actualizar el registro {category.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{id:int}", Name = "deleteCategory")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        public IActionResult DeleteCategory(int id)
        {

            if (!_categoryRepository.CategoryExists(id))
            {
                return NotFound($"La categoría con el id {id} no existe");
            }
            var category = _categoryRepository.GetCategory(id);

            if (category == null)
            {
                return NotFound($"La categoría con el id {id} no existe");
            }

            if (!_categoryRepository.DeleteCategory(category))
            {
                ModelState.AddModelError("CustomError", $"Algo salió mal al eliminar el registro {category.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }

}
