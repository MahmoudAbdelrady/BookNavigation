using aspcorebackend.Dtos;
using aspcorebackend.Services;
using aspcorebackend.Utils.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspcorebackend.Controllers {
    [Route("api/authors")]
    [ApiController]
    [Authorize]
    public class AuthorController : ControllerBase {
        private readonly IAuthorService authorService;
        public AuthorController(IAuthorService authorService) {
            this.authorService = authorService;
        }

        [HttpGet]
        public IActionResult GetAllAuthors(int? page) {
            Response<object> response = authorService.GetAllAuthors(page);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetAuthorById(int id) {
            Response<object> response = authorService.GetAuthorById(id);
            if (response.Status == "success") {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost]
        public IActionResult AddAuthor(AuthorDTO authorDTO) {
            if (ModelState.IsValid) {
                Response<object> response = authorService.AddAuthor(authorDTO);
                if (response.Status == "success") {
                    return StatusCode(201, response);
                }
                return Conflict(response);
            }
            return BadRequest(ResponseMaker<object>.ErrorRes("Validation error", ModelState));
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateAuthor(int id, AuthorDTO authorDTO) {
            if (ModelState.IsValid) {
                Response<object> response = authorService.UpdateAuthor(id, authorDTO);
                if (response.Status == "success") {
                    return Ok(response);
                }
                return NotFound(response);
            }
            return BadRequest(ResponseMaker<object>.ErrorRes("Validation error", ModelState));
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteAuthor(int id) {
            Response<object> response = authorService.DeleteAuthor(id);
            if (response.Status == "success") {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
