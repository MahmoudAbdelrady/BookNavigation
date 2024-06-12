using aspcorebackend.Dtos;
using aspcorebackend.Models;
using aspcorebackend.Repositories;
using aspcorebackend.Utils.Response;
using AutoMapper;

namespace aspcorebackend.Services.Impl {
    public class BookService : IBookService {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;
        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository,
            ICategoryRepository categoryRepository, IMapper mapper) {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }

        public Response<object> GetAllBooks(int page) {
            try {
                List<Book> books = bookRepository.FetchAllBooks(page);
                List<BookDTO> bookDTOs = books.Select(mapper.Map<BookDTO>).ToList();
                return ResponseMaker<object>.SuccessRes("Books retrieved successfully", bookDTOs);
            } catch (Exception) {
                throw new Exception("Failed to retrieve the books");
            }
        }

        public Response<object> GetBookById(int id) {
            try {
                Book? book = bookRepository.FetchBookById(id);
                if (book == null) {
                    return ResponseMaker<object>.ErrorRes("Book not found", null);
                }
                BookDTO bookDTO = mapper.Map<BookDTO>(book);
                return ResponseMaker<object>.SuccessRes("Book retrieved successfully", bookDTO);
            } catch (Exception) {
                throw new Exception("Failed to retrieve the requested book");
            }
        }

        public Response<object> AddBook(BookDTO bookDTO) {
            try {
                Book? existingBook = bookRepository.GetBookByTitle(bookDTO.Title);
                if (existingBook != null) {
                    return ResponseMaker<object>.ErrorRes("Book already exists");
                }
                // @TODO: Check book category and author
                Book book = mapper.Map<Book>(bookDTO);
                book = bookRepository.AddBook(book);
                mapper.Map(book, bookDTO);
                return ResponseMaker<object>.SuccessRes("Book added successfully", bookDTO);
            } catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public Response<object> UpdateBook(int id, BookDTO bookDTO) {
            try {
                Book? existingBook = bookRepository.GetBookById(id);
                if (existingBook == null) {
                    return ResponseMaker<object>.ErrorRes("Book not found");
                }
                bookDTO.BookId = id;
                existingBook = mapper.Map(bookDTO, existingBook);
                // @TODO: Update author and category
                bookRepository.UpdateBook();
                return ResponseMaker<object>.SuccessRes("Book updated successfully", bookDTO);
            } catch (Exception) {
                throw new Exception("Failed to update the book");
            }
        }

        public Response<object> DeleteBook(int id) {
            try {
                Book? existingBook = bookRepository.GetBookById(id);
                if (existingBook == null) {
                    return ResponseMaker<object>.ErrorRes("Book not found");
                }
                bookRepository.DeleteBook(existingBook);
                return ResponseMaker<object>.SuccessRes("Book deleted successfully", null);
            } catch (Exception) {
                throw new Exception("Failed to delete the book");
            }
        }

        public Response<object> AddBookToAuthor(int bookId, int authorId) {
            try {
                Book? book = bookRepository.GetBookById(bookId);
                if (book == null) {
                    return ResponseMaker<object>.ErrorRes("Book not found");
                }
                Author? author = authorRepository.GetAuthorById(authorId);
                if (author == null) {
                    return ResponseMaker<object>.ErrorRes("Author not found");
                }
                if (book.AuthorId == authorId) {
                    return ResponseMaker<object>.ErrorRes("Book is already assigned to this author");
                }
                bookRepository.AddBookToAuthor(book, authorId);
                //authorRepository.AddBookToAuthor(book, author);
                return ResponseMaker<object>.SuccessRes("Book is successfully assigned to the author", "No data");
            } catch (Exception) {
                throw new Exception("Failed to add the book to the author");
            }
        }

        public Response<object> AddBookToCategory(int bookId, int categoryId) {
            try {
                Book? book = bookRepository.GetBookById(bookId);
                if (book == null) {
                    return ResponseMaker<object>.ErrorRes("Book not found");
                }
                Category? category = categoryRepository.GetCategoryById(categoryId);
                if (category == null) {
                    return ResponseMaker<object>.ErrorRes("Category not found");
                }
                if (book.CategoryId == categoryId) {
                    return ResponseMaker<object>.ErrorRes($"Book already exists in {category.Title} category");
                }
                bookRepository.AddBookToCategory(book, categoryId);
                //categoryRepository.AddBookToCategory(book, category);
                return ResponseMaker<object>.SuccessRes($"Book added successfully to {category.Title} category", "No data");
            } catch (Exception) {
                throw new Exception("Failed to add the book to this category");
            }
        }
    }
}
