package com.booknav.springbackend.services;

import com.booknav.springbackend.dto.Book.BookDTO;
import com.booknav.springbackend.dto.CategoryDTO;
import com.booknav.springbackend.entities.Author;
import com.booknav.springbackend.entities.Book;
import com.booknav.springbackend.entities.Category;
import com.booknav.springbackend.repos.AuthorRepository;
import com.booknav.springbackend.repos.BookRepository;
import com.booknav.springbackend.repos.CategoryRepository;
import com.booknav.springbackend.utils.mappers.BookMapper;
import com.booknav.springbackend.utils.response.Response;
import org.modelmapper.ModelMapper;
import org.modelmapper.convention.MatchingStrategies;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@Service
public class BookService {
    private final BookRepository bookRepository;
    private final CategoryRepository categoryRepository;
    private final AuthorRepository authorRepository;
    private final BookMapper bookMapper;
    private final ModelMapper modelMapper;

    @Autowired
    public BookService(BookRepository bookRepository, CategoryRepository categoryRepository, AuthorRepository authorRepository, ModelMapper modelMapper) {
        this.bookRepository = bookRepository;
        this.categoryRepository = categoryRepository;
        this.authorRepository = authorRepository;
        this.bookMapper = new BookMapper();
        this.modelMapper = modelMapper;
    }

    private boolean checkBookCat(BookDTO bookDTO, Book book) {
        if (bookDTO.getBookCategory() != null) {
            Optional<Category> catChecker = categoryRepository.findById(bookDTO.getBookCategory().getCatId());
            if (catChecker.isPresent()) {
                book.setCategory(catChecker.get());
                return false;
            } else {
                return true;
            }
        }
        return false;
    }

    public ResponseEntity<Object> getAllBooks() throws Exception {
        try {
            List<Book> books = bookRepository.fetchAllBooks();
            List<BookDTO> bookDTOS = books
                    .stream()
                    .map(bookMapper::convertToDTO)
                    .collect(Collectors.toList());

            return ResponseEntity.ok(Response.successRes("Books retrieved successfully", bookDTOS));
        } catch (Exception e) {
            throw new Exception("Failed to retrieve the books");
        }
    }

    public ResponseEntity<Object> getBookById(int id) throws Exception {
        try {
            Book book = bookRepository.fetchSingleBook(id);
            System.out.println(book);
            if (book != null) {
                BookDTO bookDTO = bookMapper.convertToDTO(book);
                return ResponseEntity.ok(Response.successRes("Book retrieved successfully", bookDTO));
            } else {
                return ResponseEntity.status(HttpStatus.NOT_FOUND).body(Response.errorRes("Book not found", null));
            }
        } catch (Exception e) {
            System.out.println(e.getMessage());
            throw new Exception("Failed to retrieve the requested book");
        }
    }

    public ResponseEntity<Object> saveBook(BookDTO bookDTO, String message) throws Exception {
        try {
            if (bookDTO.getBookId() == null) {
                Book book = new Book();
                if (checkBookCat(bookDTO, book)) {
                    return ResponseEntity
                            .status(HttpStatus.NOT_FOUND)
                            .body(Response.errorRes("Failed to save the book - Category not found", null));
                }
                modelMapper.map(bookDTO, book);
                book = bookRepository.save(book);
                bookDTO = bookMapper.convertToDTO(book);
                return ResponseEntity
                        .status(HttpStatus.CREATED)
                        .body(Response.successRes(message, bookDTO));
            } else {
                Optional<Book> book = bookRepository.findById(bookDTO.getBookId());
                if (book.isPresent()) {
                    if (checkBookCat(bookDTO, book.get())) {
                        return ResponseEntity
                                .status(HttpStatus.NOT_FOUND)
                                .body(Response.errorRes("Failed to edit the book - Category not found", null));
                    }
                    modelMapper.map(bookDTO, book.get());
                    bookRepository.save(book.get());
                    bookDTO = bookMapper.convertToDTO(book.get());
                    return ResponseEntity.ok(Response.successRes(message, bookDTO));
                } else {
                    return ResponseEntity
                            .status(HttpStatus.NOT_FOUND)
                            .body(Response.errorRes("Book not found", null));
                }
            }
        } catch (Exception ex) {
            throw new Exception("Failed to save the book");
        }
    }

    public ResponseEntity<Object> addBookToCategory(int catId, int bookId) throws Exception {
        try {
            Optional<Book> book = bookRepository.findById(bookId);
            if (book.isPresent()) {
                Optional<Category> category = categoryRepository.findById(catId);
                if (category.isPresent()) {
                    Category existingCategory = categoryRepository.findCategoryContainsBook(catId, bookId);
                    if (existingCategory == null) {
                        category.get().addBook(book.get());
                        book.get().setCategory(category.get());
                        categoryRepository.save(category.get());
                        bookRepository.save(book.get());
                        CategoryDTO categoryDTO = modelMapper.map(category.get(), CategoryDTO.class);
                        return ResponseEntity
                                .ok(Response.successRes("Book added successfully to " + categoryDTO.getTitle() + " category", null));
                    } else {
                        return ResponseEntity
                                .status(HttpStatus.CONFLICT)
                                .body(Response.errorRes("Book already exists in " + existingCategory.getTitle() + "category", null));
                    }
                } else {
                    return ResponseEntity
                            .status(HttpStatus.NOT_FOUND)
                            .body(Response.errorRes("Category not found", null));
                }
            } else {
                return ResponseEntity
                        .status(HttpStatus.NOT_FOUND)
                        .body(Response.errorRes("Book not found", null));
            }
        } catch (Exception e) {
            throw new Exception("Failed to add the book the the category");
        }
    }

    public ResponseEntity<Object> addBookToAuthor(int authorId, int bookId) throws Exception {
        try {
            Optional<Book> book = bookRepository.findById(bookId);
            if (book.isPresent()) {
                Optional<Author> author = authorRepository.findById(authorId);
                if (author.isPresent()) {
                    Author existingAuthor = authorRepository.findAuthorContainBook(authorId, bookId);
                    if (existingAuthor == null) {
                        author.get().addBook(book.get());
                        book.get().setAuthor(author.get());
                        authorRepository.save(author.get());
                        bookRepository.save(book.get());
                        return ResponseEntity.ok(Response.successRes("Book is successfully assigned to the author", "No data"));
                    } else {
                        return ResponseEntity
                                .status(HttpStatus.CONFLICT)
                                .body(Response.errorRes("Book is already assigned to this author", null));
                    }
                } else {
                    return ResponseEntity
                            .status(HttpStatus.NOT_FOUND)
                            .body(Response.errorRes("Author not found", null));
                }
            } else {
                return ResponseEntity
                        .status(HttpStatus.NOT_FOUND)
                        .body(Response.errorRes("Book not found", null));
            }
        } catch (Exception e) {
            throw new Exception("Failed to assign the book the author");
        }
    }

    public ResponseEntity<Object> deleteBookById(int id) throws Exception {
        try {
            Optional<Book> book = bookRepository.findById(id);
            if (book.isPresent()) {
                bookRepository.deleteById(id);
                return ResponseEntity.ok(Response.successRes("Book deleted successfully", "No data"));
            } else {
                return ResponseEntity
                        .status(HttpStatus.NOT_FOUND)
                        .body(Response.errorRes("Book not found", null));
            }
        } catch (Exception e) {
            throw new Exception("Failed to delete the book");
        }
    }
}
