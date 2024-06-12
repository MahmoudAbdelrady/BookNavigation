package com.booknav.springbackend.services;

import com.booknav.springbackend.dto.Book.BookDTO;
import com.booknav.springbackend.dto.CategoryDTO;
import com.booknav.springbackend.entities.Book;
import com.booknav.springbackend.entities.Category;
import com.booknav.springbackend.repos.CategoryRepository;
import com.booknav.springbackend.utils.response.Response;
import org.modelmapper.ModelMapper;
import org.modelmapper.TypeMap;
import org.modelmapper.TypeToken;
import org.modelmapper.convention.MatchingStrategies;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import java.lang.reflect.Type;
import java.util.List;
import java.util.Optional;

@Service
public class CategoryService {
    private final CategoryRepository categoryRepository;
    private final ModelMapper modelMapper;

    @Autowired
    public CategoryService(CategoryRepository categoryRepository, ModelMapper modelMapper) {
        this.categoryRepository = categoryRepository;
        this.modelMapper = modelMapper;
    }

    public ResponseEntity<Object> getAllCategories() throws Exception {
        try {
            List<Category> categories = categoryRepository.findAll();
            Type listType = new TypeToken<List<CategoryDTO>>() {
            }.getType();

            TypeMap<Category, CategoryDTO> typeMap = modelMapper.getTypeMap(Category.class, CategoryDTO.class);
            if (typeMap == null) {
                typeMap = modelMapper.createTypeMap(Category.class, CategoryDTO.class);
            }
            typeMap.addMappings(mapping -> mapping.skip(CategoryDTO::setBooks));

            List<CategoryDTO> categoryDTOS = modelMapper.map(categories, listType);
            return ResponseEntity.ok(Response.successRes("Categories retrieved successfully", categoryDTOS));
        } catch (Exception e) {
            throw new Exception("Failed to retrieve categories");
        }
    }

    public ResponseEntity<Object> getCategoryById(int id) throws Exception {
        try {
            Optional<Category> category = categoryRepository.findById(id);
            if (category.isPresent()) {
                CategoryDTO categoryDTO = modelMapper.map(category.get(), CategoryDTO.class);
                return ResponseEntity.ok(Response.successRes("Category retrieved successfully", categoryDTO));
            } else {
                return ResponseEntity
                        .status(HttpStatus.NOT_FOUND)
                        .body(Response.errorRes("Category not found", null));
            }
        } catch (Exception e) {
            throw new Exception("Failed to retrieve the category");
        }
    }

    public ResponseEntity<Object> saveCategory(CategoryDTO categoryDTO, String message) throws Exception {
        try {
            if (categoryDTO.getCategoryId() == null) {
                Category category = modelMapper.map(categoryDTO, Category.class);
                categoryRepository.save(category);
                modelMapper.map(category, categoryDTO);
                return ResponseEntity
                        .status(HttpStatus.CREATED)
                        .body(Response.successRes(message, categoryDTO));
            } else {
                Optional<Category> category = categoryRepository.findById(categoryDTO.getCategoryId());
                if (category.isPresent()) {
                    modelMapper.map(categoryDTO, category.get());
                    categoryRepository.save(category.get());
                    modelMapper.map(category.get(), categoryDTO);
                    return ResponseEntity.ok(Response.successRes(message, categoryDTO));
                } else {
                    return ResponseEntity
                            .status(HttpStatus.NOT_FOUND)
                            .body(Response.errorRes("Category not found", null));
                }
            }
        } catch (Exception e) {
            throw new Exception("Failed to save the category");
        }
    }

    public ResponseEntity<Object> saveCategoryBooks(List<BookDTO> bookDTOS, int catId) throws Exception {
        try {
            Optional<Category> category = categoryRepository.findById(catId);
            if (category.isPresent()) {
                Type listType = new TypeToken<List<Book>>() {
                }.getType();
                List<Book> books = modelMapper.map(bookDTOS, listType);
                category.get().setBooks(books);
                categoryRepository.save(category.get());
                return ResponseEntity
                        .ok(Response.successRes("Books added successfully to " + category.get().getTitle() + " category", "No data"));
            } else {
                return ResponseEntity
                        .status(HttpStatus.NOT_FOUND)
                        .body(Response.errorRes("Category not found", null));
            }
        } catch (Exception e) {
            throw new Exception("Failed to add the books to the category");
        }
    }

    public ResponseEntity<Object> deleteCategoryById(int id) throws Exception {
        try {
            Optional<Category> category = categoryRepository.findById(id);
            if (category.isPresent()) {
                categoryRepository.deleteById(id);
                return ResponseEntity.ok(Response.successRes("Category deleted successfully", "No data"));
            } else {
                return ResponseEntity
                        .status(HttpStatus.NOT_FOUND)
                        .body(Response.errorRes("Category not found", null));
            }
        } catch (Exception e) {
            throw new Exception("Failed to delete the category");
        }
    }
}
