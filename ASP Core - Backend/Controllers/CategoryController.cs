using aspcorebackend.Dtos;
using aspcorebackend.Services;
using aspcorebackend.Utils.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspcorebackend.Controllers {
    [Route("api/categories")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase {
        private readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService) {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAllCategories(int? page) {
            Response<object> response = categoryService.GetAllCategories(page);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCategoryById(int id) {
            Response<object> response = categoryService.GetCategoryById(id);
            if (response.Status == "success") {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost]
        public IActionResult AddCategory(CategoryDTO categoryDTO) {
            Response<object> response = categoryService.AddCategory(categoryDTO);
            if (response.Status == "success") {
                return StatusCode(201, response);
            }
            return Conflict(response);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateCategory(int id, CategoryDTO categoryDTO) {
            Response<object> response = categoryService.UpdateCategory(id, categoryDTO);
            if (response.Status == "success") {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteCategory(int id) {
            Response<object> response = categoryService.DeleteCategory(id);
            if (response.Status == "success") {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
