package com.booknav.springbackend.repos;

import com.booknav.springbackend.entities.Category;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;

public interface CategoryRepository extends JpaRepository<Category, Integer> {
    @Query("SELECT c FROM Category c JOIN FETCH c.books b WHERE c.categoryId = :catId AND b.bookId = :bookId")
    Category findCategoryContainsBook(@Param("catId") int catId, @Param("bookId") int bookId);
}
