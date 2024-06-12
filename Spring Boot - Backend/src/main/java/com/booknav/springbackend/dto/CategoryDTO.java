package com.booknav.springbackend.dto;

import com.booknav.springbackend.dto.Book.BookDTO;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
public class CategoryDTO {
    private Integer categoryId;
    @NotBlank(message = "Category title is required")
    @Size(max = 100, message = "Category title can't exceed 100 characters.")
    private String title;
    private List<BookDTO> books;
}
