package com.booknav.springbackend.entities;

import jakarta.persistence.*;
import jakarta.validation.constraints.*;
import lombok.Getter;
import lombok.Setter;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "books")
@Getter
@Setter
public class Book {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "book_id")
    private Integer bookId;
    @NotBlank(message = "Book title is required")
    @Size(max = 150, message = "Book title can't exceed 150 characters.")
    private String title;
    @NotBlank(message = "Book description is required")
    @Size(max = 300, message = "Book description can't exceed 300 characters.")
    private String description;
    @Column(name = "publish_year")
    @NotBlank(message = "Book publish year is required")
    @Pattern(regexp = "\\b(1[0-9]{3}|[2-9][0-9]{3})\\b", message = "Please enter a valid year")
    private Integer publishYear;
    @NotBlank(message = "Book cover's image link is required")
    private String cover;
    @NotBlank(message = "Book price is required")
    @Min(value = 1, message = "Book price can't be less than 1")
    @Max(value = 3000, message = "Book price can't exceed 3000")
    private BigDecimal price;
    @ManyToOne
    @JoinColumn(name = "author_id")
    private Author author;
    @ManyToOne(fetch = FetchType.LAZY, cascade = {CascadeType.PERSIST, CascadeType.MERGE, CascadeType.DETACH, CascadeType.REFRESH})
    @JoinColumn(name = "cat_id")
    private Category category;
    @ManyToMany(fetch = FetchType.LAZY, cascade = {CascadeType.PERSIST, CascadeType.MERGE, CascadeType.DETACH, CascadeType.REFRESH})
    @JoinTable(name = "book_users",
            joinColumns = @JoinColumn(name = "book_id"),
            inverseJoinColumns = @JoinColumn(name = "user_id"))
    private List<User> users;
    @Column(name = "added_at")
    private LocalDateTime addedAt;

    public Book() {
        this.addedAt = LocalDateTime.now();
    }

    public Book(String title, String description, int publishYear, String cover, BigDecimal price, Author author, Category category) {
        this.title = title;
        this.description = description;
        this.publishYear = publishYear;
        this.cover = cover;
        this.price = price;
        this.author = author;
        this.category = category;
        this.addedAt = LocalDateTime.now();
    }
}
