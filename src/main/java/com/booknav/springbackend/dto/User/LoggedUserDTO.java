package com.booknav.springbackend.dto.User;

import jakarta.validation.constraints.Email;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Pattern;
import jakarta.validation.constraints.Size;
import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;

@Getter
@Setter
public class LoggedUserDTO {
    private Integer userId;
    @NotBlank(message = "Username is required")
    @Pattern(regexp = "^[a-zA-Z0-9_]{4,16}$", message = "Username can't contain any special characters")
    private String username;
    @Size(max = 100, message = "Firstname can't exceed 100 characters")
    private String firstName;
    @Size(max = 100, message = "Lastname can't exceed 100 characters")
    private String lastName;
    @NotBlank(message = "Email is required")
    @Email
    private String email;
    @NotBlank(message = "Token is required")
    @Pattern(regexp = "^[A-Za-z0-9-_]+\\.[A-Za-z0-9-_]+\\.[A-Za-z0-9-_]+$", message = "Valid token is required")
    private String token;
    private LocalDateTime createdAt;
}
