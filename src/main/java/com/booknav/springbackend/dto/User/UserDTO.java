package com.booknav.springbackend.dto.User;

import jakarta.validation.constraints.*;
import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;

@Getter
@Setter
public class UserDTO {
    private Integer userId;
    @NotBlank(message = "Username is required")
    @Pattern(regexp = "^[a-zA-Z0-9_]{4,16}$", message = "Username can't contain any special characters")
    @Size(min = 4, message = "Username must be at least 4 characters")
    @Size(max = 64, message = "Username can't exceed 64 characters")
    private String username;
    @Size(max = 100, message = "Firstname can't exceed 100 characters")
    private String firstName;
    @Size(max = 100, message = "Lastname can't exceed 100 characters")
    private String lastName;
    @NotBlank(message = "Email is required")
    @Email(message = "Please enter a valid email address")
    private String email;
    @NotBlank(message = "Password is required")
    @Pattern(regexp = "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$",
            message = "Password must be 8 characters at least and contains: 1 Uppercase letter, 1 Lowercase letter, 1 Special character, 1 digit")
    private String password;
    private LocalDateTime createdAt;
}
