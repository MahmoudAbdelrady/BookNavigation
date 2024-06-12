package com.booknav.springbackend.repos;

import com.booknav.springbackend.entities.Book;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

import java.util.List;

public interface BookRepository extends JpaRepository<Book, Integer> {
    @Query("SELECT b FROM Book b LEFT JOIN FETCH b.category LEFT JOIN FETCH b.author")
    List<Book> fetchAllBooks();
    @Query("SELECT b FROM Book b LEFT JOIN FETCH b.category LEFT JOIN FETCH b.author WHERE b.bookId = :id")
    Book fetchSingleBook(@Param("id") int id);
}
