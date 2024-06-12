package com.booknav.springbackend.dto;

import com.booknav.springbackend.dto.Book.BookDTO;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;
import java.util.List;

@Getter
@Setter
public class AuthorDTO {
    private Integer authorId;
    @NotBlank(message = "Author's firstname is required")
    @Size(max = 100, message = "Author's firstname can't exceed 100 characters")
    private String firstName;
    @NotBlank(message = "Author's lastname is required")
    @Size(max = 100, message = "Author's lastname can't exceed 100 characters.")
    private String lastName;
    private List<BookDTO> books;
    private LocalDateTime addedAt;
}
