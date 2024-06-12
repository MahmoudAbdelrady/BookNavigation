package com.booknav.springbackend.dto.Book;

import jakarta.validation.constraints.*;
import lombok.Getter;
import lombok.Setter;

import java.math.BigDecimal;

@Getter
@Setter
public class BookDTO {
    private Integer bookId;
    @NotBlank(message = "Book title is required")
    @Size(max = 150, message = "Book title can't exceed 150 characters.")
    private String title;
    @NotBlank(message = "Book description is required")
    @Size(max = 300, message = "Book description can't exceed 300 characters.")
    private String description;
    @NotBlank(message = "Book publish year is required")
    @Pattern(regexp = "\\b(1[0-9]{3}|[2-9][0-9]{3})\\b", message = "Please enter a valid year")
    private Integer publishYear;
    @NotBlank(message = "Book cover's image link is required")
    private String cover;
    @NotBlank(message = "Book price is required")
    @Min(value = 1, message = "Book price can't be less than 1")
    @Max(value = 3000, message = "Book price can't exceed 3000")
    private BigDecimal price;
    private String bookAuthor;
    private BookCatReqDTO bookCategory;
}
