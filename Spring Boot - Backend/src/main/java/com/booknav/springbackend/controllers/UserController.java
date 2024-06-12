package com.booknav.springbackend.controllers;

import com.booknav.springbackend.dto.User.UserDTO;
import com.booknav.springbackend.services.UserService;
import jakarta.validation.Valid;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@RestController
public class UserController {
    private final UserService userService;

    @Autowired
    public UserController(UserService userService) {
        this.userService = userService;
    }

    @GetMapping("/users")
    public ResponseEntity<Object> getAllUsers() throws Exception {
        return userService.getAllUsers();
    }

    @GetMapping("/users/{id}")
    public ResponseEntity<Object> getUserById(@PathVariable int id) throws Exception {
        return userService.getUserById(id);
    }

    @PostMapping("/users")
    public ResponseEntity<Object> signup(@RequestBody @Valid UserDTO userDTO) throws Exception {
        return userService.saveUser(userDTO, "Account created successfully");
    }

    @PutMapping("/users/{id}")
    public ResponseEntity<Object> editInfo(@RequestBody UserDTO updatedUser, @PathVariable int id) throws Exception {
        updatedUser.setUserId(id);
        return userService.saveUser(updatedUser, "User information updated successfully");
    }

    @DeleteMapping("/users/{id}")
    public ResponseEntity<Object> deleteUserById(@PathVariable int id) throws Exception {
        return userService.deleteUserById(id);
    }
}
