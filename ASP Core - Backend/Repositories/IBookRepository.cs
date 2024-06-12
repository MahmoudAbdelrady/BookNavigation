using aspcorebackend.Models;

namespace aspcorebackend.Repositories {
    public interface IBookRepository {
        List<Book> GetAllBooks(int? page);
        List<Book> FetchAllBooks(int? page);
        Book? GetBookById(int id);
        Book? FetchBookById(int id);
        Book? GetBookByTitle(string title);
        Book AddBook(Book book);
        void UpdateBook();
        void DeleteBook(Book existingBook);
        void AddBookToAuthor(Book book, int authorId);
        void AddBookToCategory(Book book, int categoryId);
    }
}
