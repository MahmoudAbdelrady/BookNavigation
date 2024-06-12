using aspcorebackend.Config;
using aspcorebackend.Models;
using Microsoft.EntityFrameworkCore;

namespace aspcorebackend.Repositories.Impl {
    public class BookRepository : IBookRepository {
        private readonly BookNavDbContext dbContext;
        public BookRepository(BookNavDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public List<Book> GetAllBooks(int? page) {
            if (page < 1 || page == null) page = 1;
            int skip = (int)(page - 1) * 10;
            List<Book> books = dbContext.Books.Skip(skip).Take(10).ToList();
            return books;
        }

        public List<Book> FetchAllBooks(int? page) {
            if (page < 1 || page == null) page = 1;
            int skip = (int)(page - 1) * 10;
            List<Book> books = dbContext.Books.Include(b => b.Author).Include(b => b.Category).Skip(skip).Take(10).ToList();
            return books;
        }

        public Book? GetBookById(int id) {
            Book? book = dbContext.Books.FirstOrDefault(b => b.BookId == id);
            return book;
        }

        public Book? FetchBookById(int id) {
            Book? book = dbContext.Books.Include(b => b.Author).Include(b => b.Category).FirstOrDefault(b => b.BookId == id);
            return book;
        }

        public Book? GetBookByTitle(string title) {
            Book? book = dbContext.Books.FirstOrDefault(b => b.Title.ToLower() == title.ToLower());
            return book;
        }

        public Book AddBook(Book book) {
            dbContext.Books.Add(book);
            dbContext.SaveChanges();
            return book;
        }

        public void UpdateBook() {
            dbContext.SaveChanges();
        }

        public void DeleteBook(Book existingBook) {
            dbContext.Books.Remove(existingBook);
            dbContext.SaveChanges();
        }

        public void AddBookToAuthor(Book book, int authorId) {
            book.AuthorId = authorId;
            dbContext.SaveChanges();
        }

        public void AddBookToCategory(Book book, int categoryId) {
            book.CategoryId = categoryId;
            dbContext.SaveChanges();
        }
    }
}
