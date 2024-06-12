package com.booknav.springbackend.controllers;

import com.booknav.springbackend.dto.Book.BookAuthorReqDTO;
import com.booknav.springbackend.dto.Book.BookCatReqDTO;
import com.booknav.springbackend.dto.Book.BookDTO;
import com.booknav.springbackend.services.BookService;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
public class BookController {
    private final BookService bookService;

    @Autowired
    public BookController(BookService bookService) {
        this.bookService = bookService;
    }

    @GetMapping("/books")
    public ResponseEntity<Object> getAllBooks() throws Exception {
        return bookService.getAllBooks();
    }

    @GetMapping("/books/{id}")
    public ResponseEntity<Object> getBookById(@PathVariable int id) throws Exception {
        return bookService.getBookById(id);
    }

    @PostMapping("/books")
    public ResponseEntity<Object> addBook(@RequestBody @Valid BookDTO bookDTO) throws Exception {
        return bookService.saveBook(bookDTO, "Book added successfully");
    }

    @PutMapping("/books/{id}")
    public ResponseEntity<Object> editBook(@RequestBody BookDTO updatedBook, @PathVariable int id) throws Exception {
        updatedBook.setBookId(id);
        return bookService.saveBook(updatedBook, "Book information updated successfully");
    }

    @PostMapping("/books/{bookId}")
    public ResponseEntity<Object> addBookToCategory(@RequestBody @Valid BookCatReqDTO cat, @PathVariable int bookId) throws Exception {
        return bookService.addBookToCategory(cat.getCatId(), bookId);
    }

    @PostMapping("/books/{bookId}/author")
    public ResponseEntity<Object> addBookToAuthor(@RequestBody @Valid BookAuthorReqDTO bookAuthor, @PathVariable int bookId) throws Exception {
        return bookService.addBookToAuthor(bookAuthor.getAuthorId(), bookId);
    }

    @DeleteMapping("/books/{id}")
    public ResponseEntity<Object> deleteBookById(@PathVariable int id) throws Exception {
        return bookService.deleteBookById(id);
    }
}
