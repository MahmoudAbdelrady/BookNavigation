using aspcorebackend.Dtos;
using aspcorebackend.Services;
using aspcorebackend.Utils.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspcorebackend.Controllers {
    [Route("api/books")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase {
        private readonly IBookService bookService;
        public BookController(IBookService bookService) {
            this.bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetAllBooks([FromQuery] int page) {
            Response<object> response = bookService.GetAllBooks(page);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetBookById(int id) {
            Response<object> response = bookService.GetBookById(id);
            if (response.Status == "success") {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost]
        public IActionResult AddBook(BookDTO bookDTO) {
            if (ModelState.IsValid) {
                Response<object> response = bookService.AddBook(bookDTO);
                if (response.Status == "success") {
                    return StatusCode(201, response);
                }
                return Conflict(response);
            }
            return BadRequest(ResponseMaker<object>.ErrorRes("Validation error", ModelState));
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateBook(int id, BookDTO bookDTO) {
            if (ModelState.IsValid) {
                Response<object> response = bookService.UpdateBook(id, bookDTO);
                if (response.Status == "success") {
                    return Ok(response);
                }
                return NotFound(response);
            }
            return BadRequest(ResponseMaker<object>.ErrorRes("Validation error", ModelState));
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteBook(int id) {
            Response<object> response = bookService.DeleteBook(id);
            if (response.Status == "success") {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost("{bookId:int}/authors/{authorId:int}")]
        public IActionResult AddBookToAuthor(int bookId, int authorId) {
            Response<object> response = bookService.AddBookToAuthor(bookId, authorId);
            if (response.Status == "success") {
                return Ok(response);
            }
            return NotFound(response);
        }

        [HttpPost("{bookId:int}/categories/{categoryId:int}")]
        public IActionResult AddBookToCategory(int bookId, int categoryId) {
            Response<object> response = bookService.AddBookToCategory(bookId, categoryId);
            if (response.Status == "success") {
                return Ok(response);
            }
            return NotFound(response);
        }
    }
}
