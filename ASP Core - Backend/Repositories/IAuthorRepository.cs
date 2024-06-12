using aspcorebackend.Models;

namespace aspcorebackend.Repositories {
    public interface IAuthorRepository {
        List<Author> GetAllAuthors(int? page);
        Author? GetAuthorById(int id);
        Author? GetAuthorWithBooksById(int id);
        Author AddAuthor(Author author);
        void UpdateAuthor();
        void DeleteAuthor(Author author);
    }
}
