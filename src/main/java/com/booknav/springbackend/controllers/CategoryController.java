package com.booknav.springbackend.controllers;

import com.booknav.springbackend.dto.Book.BookDTO;
import com.booknav.springbackend.dto.CategoryDTO;
import com.booknav.springbackend.services.CategoryService;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
public class CategoryController {
    private final CategoryService categoryService;

    @Autowired
    public CategoryController(CategoryService categoryService) {
        this.categoryService = categoryService;
    }

    @GetMapping("/categories")
    public ResponseEntity<Object> getAllCategories() throws Exception {
        return categoryService.getAllCategories();
    }

    @GetMapping("/categories/{id}")
    public ResponseEntity<Object> getCategoryById(@PathVariable int id) throws Exception {
        return categoryService.getCategoryById(id);
    }

    @PostMapping("/categories")
    public ResponseEntity<Object> addCategory(@RequestBody @Valid CategoryDTO categoryDTO) throws Exception {
        return categoryService.saveCategory(categoryDTO, "Category added successfully");
    }

    @PutMapping("/categories/{id}")
    public ResponseEntity<Object> editCategory(@RequestBody CategoryDTO updatedCategory, @PathVariable int id) throws Exception {
        updatedCategory.setCategoryId(id);
        return categoryService.saveCategory(updatedCategory, "Category information updated successfully");
    }

    @PostMapping("/categories/{id}")
    public ResponseEntity<Object> addBooksToCategory(@RequestBody List<BookDTO> bookDTOS, @PathVariable int id) throws Exception {
        return categoryService.saveCategoryBooks(bookDTOS, id);
    }

    @DeleteMapping("/categories/{id}")
    public ResponseEntity<Object> deleteCategory(@PathVariable int id) throws Exception {
        return categoryService.deleteCategoryById(id);
    }
}
