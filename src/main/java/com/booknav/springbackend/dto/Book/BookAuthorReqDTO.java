package com.booknav.springbackend.dto.Book;

import jakarta.validation.constraints.NotBlank;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class BookAuthorReqDTO {
    @NotBlank(message = "Author ID is required")
    private int authorId;
}
