package com.booknav.springbackend.controllers;

import com.booknav.springbackend.dto.AuthorDTO;
import com.booknav.springbackend.services.AuthorService;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
public class AuthorController {
    private final AuthorService authorService;

    @Autowired
    public AuthorController(AuthorService authorService) {
        this.authorService = authorService;
    }

    @GetMapping("/authors")
    public ResponseEntity<Object> getAllAuthors() {
        return authorService.getAllAuthors();
    }

    @GetMapping("/authors/{id}")
    public ResponseEntity<Object> getAuthorById(@PathVariable int id) {
        return authorService.getAuthorById(id);
    }

    @PostMapping("/authors")
    public ResponseEntity<Object> addAuthor(@RequestBody @Valid AuthorDTO authorDTO) {
        return authorService.saveAuthor(authorDTO, "Author added successfully");
    }

    @PutMapping("/authors/{id}")
    public ResponseEntity<?> editAuthor(@RequestBody AuthorDTO updatedAuthor, @PathVariable int id) {
        updatedAuthor.setAuthorId(id);
        return authorService.saveAuthor(updatedAuthor, "Author information updated successfully");
    }

    @DeleteMapping("/authors/{id}")
    public ResponseEntity<Object> deleteAuthor(@PathVariable int id) {
        return authorService.deleteAuthorById(id);
    }
}
