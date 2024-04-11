package com.booknav.springbackend.repos;

import com.booknav.springbackend.entities.User;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserRepository extends JpaRepository<User, Integer> {
    User findUserByEmailOrUsername(String email, String username);
}
