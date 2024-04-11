package com.booknav.springbackend.repos;

import com.booknav.springbackend.entities.Author;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface AuthorRepository extends JpaRepository<Author, Integer> {
    @Query("SELECT a FROM Author a JOIN FETCH a.books b WHERE a.authorId = :authorId AND b.bookId = :bookId")
    Author findAuthorContainBook(@Param("authorId") int authorId, @Param("bookId") int bookId);
}
