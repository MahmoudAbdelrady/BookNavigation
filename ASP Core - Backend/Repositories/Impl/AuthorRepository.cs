using aspcorebackend.Config;
using aspcorebackend.Models;

namespace aspcorebackend.Repositories.Impl {
    public class AuthorRepository : IAuthorRepository {
        private readonly BookNavDbContext dbContext;
        public AuthorRepository(BookNavDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public List<Author> GetAllAuthors(int? page) {
            if (page < 1 || page == null) page = 1;
            int skip = (int)((page - 1) * 10);
            List<Author> authors = dbContext.Authors.Skip(skip).Take(10).ToList();
            return authors;
        }

        public Author? GetAuthorById(int id) {
            Author? author = dbContext.Authors.FirstOrDefault(a => a.AuthorId == id);
            return author;
        }

        public Author? GetAuthorWithBooksById(int id) {
            Author? author = dbContext.Authors.FirstOrDefault(a => a.AuthorId == id);
            if (author != null) {
                List<Book> books = dbContext.Books.Where(b => b.AuthorId == id).Skip(0).Take(10).ToList();
                author.Books = books;
            }
            return author;
        }

        public Author AddAuthor(Author author) {
            dbContext.Authors.Add(author);
            dbContext.SaveChanges();
            return author;
        }

        public void UpdateAuthor() {
            dbContext.SaveChanges();
        }

        public void DeleteAuthor(Author author) {
            dbContext.Authors.Remove(author);
            dbContext.SaveChanges();
        }
    }
}
