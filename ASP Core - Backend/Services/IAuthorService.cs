using aspcorebackend.Dtos;
using aspcorebackend.Utils.Response;

namespace aspcorebackend.Services {
    public interface IAuthorService {
        Response<object> GetAllAuthors(int? page);
        Response<object> GetAuthorById(int id);
        Response<object> AddAuthor(AuthorDTO authorDTO);
        Response<object> UpdateAuthor(int id, AuthorDTO authorDTO);
        Response<object> DeleteAuthor(int id);
    }
}
