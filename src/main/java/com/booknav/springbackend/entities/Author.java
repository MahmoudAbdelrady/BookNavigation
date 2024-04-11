package com.booknav.springbackend.entities;

import jakarta.persistence.*;
import lombok.Getter;
import lombok.Setter;

import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;

@Entity
@Table(name = "authors")
@Getter
@Setter
public class Author {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "author_id")
    private int authorId;
    @Column(name = "firstname")
    private String firstName;
    @Column(name = "lastname")
    private String lastName;
    @OneToMany(mappedBy = "author")
    private List<Book> books;
    private LocalDateTime addedAt;

    public Author() {
        this.addedAt = LocalDateTime.now();
    }

    public Author(String firstName, String lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.addedAt = LocalDateTime.now();
    }

    public void addBook(Book theBook) {
        if (books == null) {
            books = new ArrayList<>();
        }
        books.add(theBook);
        theBook.setAuthor(this);
    }
}
