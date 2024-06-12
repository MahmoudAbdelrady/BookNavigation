package com.booknav.springbackend.entities;

import jakarta.persistence.*;
import jakarta.validation.constraints.NotBlank;
import jakarta.validation.constraints.Size;
import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "categories")
@Getter
@Setter
public class Category {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "category_id")
    private int categoryId;
    @NotBlank(message = "Category title is required")
    @Size(max = 100, message = "Category title can't exceed 100 characters.")
    private String title;
    @OneToMany(mappedBy = "category", fetch = FetchType.LAZY, cascade = {CascadeType.PERSIST, CascadeType.MERGE, CascadeType.DETACH, CascadeType.REFRESH})
    private List<Book> books;
    @Column(name = "added_at")
    private LocalDateTime addedAt;

    public Category() {
        this.addedAt = LocalDateTime.now();
    }

    public Category(int categoryId, String title) {
        this.categoryId = categoryId;
        this.title = title;
        this.addedAt = LocalDateTime.now();
    }

    public void addBook(Book theBook) {
        if (books == null) {
            books = new ArrayList<>();
        }
        books.add(theBook);
        theBook.setCategory(this);
    }
}
