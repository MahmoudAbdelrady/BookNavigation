using aspcorebackend.Dtos;
using aspcorebackend.Models;
using aspcorebackend.Repositories;
using aspcorebackend.Utils.Response;
using AutoMapper;

namespace aspcorebackend.Services.Impl {
    public class AuthorService : IAuthorService {
        private readonly IAuthorRepository authorRepository;
        private readonly IMapper mapper;
        public AuthorService(IAuthorRepository authorRepository, IMapper mapper) {
            this.authorRepository = authorRepository;
            this.mapper = mapper;
        }

        public Response<object> GetAllAuthors(int? page) {
            try {
                List<Author> authors = authorRepository.GetAllAuthors(page);
                List<AuthorDTO> authorDTOs = authors.Select(a => {
                    a.Books = [];
                    return mapper.Map<AuthorDTO>(a);
                }).ToList();
                return ResponseMaker<object>.SuccessRes("Authors retrieved successfully", authorDTOs);
            } catch (Exception) {
                throw new Exception("Failed to retrieve authors");
            }
        }

        public Response<object> GetAuthorById(int id) {
            try {
                Author? author = authorRepository.GetAuthorWithBooksById(id);
                if (author == null) {
                    return ResponseMaker<object>.ErrorRes("Author not found", null);
                }
                AuthorDTO authorDTO = mapper.Map<AuthorDTO>(author);
                return ResponseMaker<object>.SuccessRes("Author retrieved successfully", authorDTO);
            } catch (Exception) {
                throw new Exception("Failed to retrieve the author");
            }
        }

        public Response<object> AddAuthor(AuthorDTO authorDTO) {
            try {
                Author author = mapper.Map<Author>(authorDTO);
                author = authorRepository.AddAuthor(author);
                authorDTO = mapper.Map<AuthorDTO>(author);
                return ResponseMaker<object>.SuccessRes("Author added successfully", authorDTO);
            } catch (Exception) {
                throw new Exception("Failed to save the author");
            }
        }

        public Response<object> UpdateAuthor(int id, AuthorDTO authorDTO) {
            try {
                Author? author = authorRepository.GetAuthorWithBooksById(id);
                if (author == null) {
                    return ResponseMaker<object>.ErrorRes("Author not found", null);
                }
                author.Firstname = authorDTO.Firstname;
                author.Lastname = authorDTO.Lastname;
                authorRepository.UpdateAuthor();
                authorDTO = mapper.Map<AuthorDTO>(author);
                return ResponseMaker<object>.SuccessRes("Author information updated successfully", authorDTO);
            } catch (Exception) {
                throw new Exception("Failed to update the author information");
            }
        }

        public Response<object> DeleteAuthor(int id) {
            try {
                Author? author = authorRepository.GetAuthorWithBooksById(id);
                if (author == null) {
                    return ResponseMaker<object>.ErrorRes("Author not found");
                }
                authorRepository.DeleteAuthor(author);
                return ResponseMaker<object>.SuccessRes("Author deleted successfully", null);
            } catch (Exception) {
                throw new Exception("Failed to delete the author");
            }
        }
    }
}
