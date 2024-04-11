package com.booknav.springbackend.dto.Book;

import jakarta.validation.constraints.Min;
import jakarta.validation.constraints.NotBlank;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class BookCatReqDTO {
    @NotBlank(message = "Category ID is required")
    private int catId;
    private String catTitle;
}
