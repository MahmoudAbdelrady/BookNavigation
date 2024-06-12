using aspcorebackend.Dtos;
using aspcorebackend.Utils.Response;

namespace aspcorebackend.Services {
    public interface IBookService {
        Response<object> GetAllBooks(int page);
        Response<object> GetBookById(int id);
        Response<object> AddBook(BookDTO bookDTO);
        Response<object> UpdateBook(int id, BookDTO bookDTO);
        Response<object> DeleteBook(int id);
        Response<object> AddBookToAuthor(int bookId, int authorId);
        Response<object> AddBookToCategory(int bookId, int categoryId);
    }
}
