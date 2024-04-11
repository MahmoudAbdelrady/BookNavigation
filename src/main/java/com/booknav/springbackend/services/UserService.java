package com.booknav.springbackend.services;

import com.booknav.springbackend.dto.User.UserDTO;
import com.booknav.springbackend.entities.User;
import com.booknav.springbackend.repos.UserRepository;
import com.booknav.springbackend.utils.response.Response;
import org.modelmapper.ModelMapper;
import org.modelmapper.TypeMap;
import org.modelmapper.TypeToken;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.lang.reflect.Type;
import java.util.List;
import java.util.Objects;
import java.util.Optional;

@Service
public class UserService implements UserDetailsService {
    private final UserRepository userRepository;

    private final ModelMapper modelMapper;

    private final PasswordEncoder passwordEncoder;

    @Autowired
    public UserService(UserRepository userRepository, PasswordEncoder passwordEncoder, ModelMapper modelMapper) {
        this.userRepository = userRepository;
        this.passwordEncoder = passwordEncoder;
        this.modelMapper = modelMapper;
    }

    public ResponseEntity<Object> getAllUsers() throws Exception {
        try {
            List<User> users = userRepository.findAll();
            Type listType = new TypeToken<List<UserDTO>>() {
            }.getType();

            TypeMap<User, UserDTO> typeMap = modelMapper.getTypeMap(User.class, UserDTO.class);
            if (typeMap == null) {
                typeMap = modelMapper.createTypeMap(User.class, UserDTO.class);
            }
            typeMap.addMappings(mapping -> mapping.skip(UserDTO::setPassword));

            List<UserDTO> userDTOS = modelMapper.map(users, listType);

            return ResponseEntity.ok(Response.successRes("Users retrieved successfully", userDTOS));
        } catch (Exception e) {
            throw new Exception("Failed to retrieve users");
        }
    }

    public ResponseEntity<Object> getUserById(int id) throws Exception {
        try {
            Optional<User> user = userRepository.findById(id);
            if (user.isPresent()) {
                TypeMap<User, UserDTO> typeMap = modelMapper.getTypeMap(User.class, UserDTO.class);
                if (typeMap == null) {
                    typeMap = modelMapper.createTypeMap(User.class, UserDTO.class);
                }
                typeMap.addMappings(mapping -> mapping.skip(UserDTO::setPassword));
                UserDTO userDTO = modelMapper.map(user.get(), UserDTO.class);
                return ResponseEntity.ok(Response.successRes("User information retrieved successfully", userDTO));
            } else {
                return ResponseEntity
                        .status(HttpStatus.NOT_FOUND)
                        .body(Response.errorRes("User not found", null));
            }
        } catch (Exception e) {
            throw new Exception("Failed to retrieve user information");
        }
    }

    public ResponseEntity<Object> saveUser(UserDTO userDTO, String message) throws Exception {
        if (userDTO.getUserId() == null) {
            User user = userRepository.findUserByEmailOrUsername(userDTO.getEmail(), userDTO.getUsername());
            if (user == null) {
                try {
                    user = new User();
                    userDTO.setPassword(passwordEncoder.encode(userDTO.getPassword()));
                    modelMapper.map(userDTO, user);
                    userRepository.save(user);
                    modelMapper.map(user, userDTO);
                    return ResponseEntity
                            .status(HttpStatus.CREATED)
                            .body(Response.successRes(message, "No data"));
                } catch (Exception e) {
                    System.out.println(e.getMessage());
                    throw new Exception("Failed to create a new account");
                }
            } else {
                if (Objects.equals(user.getEmail(), userDTO.getEmail())) {
                    return ResponseEntity
                            .status(HttpStatus.CONFLICT)
                            .body(Response.errorRes("This email is already registered", null));
                } else {
                    return ResponseEntity
                            .status(HttpStatus.CONFLICT)
                            .body(Response.errorRes("This username already exists", null));
                }
            }
        } else {
            try {
                Optional<User> user = userRepository.findById(userDTO.getUserId());
                if (user.isPresent()) {
                    modelMapper.map(userDTO, user.get());
                    userRepository.save(user.get());
                    modelMapper.map(user.get(), userDTO);
                    return ResponseEntity.ok(Response.successRes(message, userDTO));
                } else {
                    return ResponseEntity
                            .status(HttpStatus.NOT_FOUND)
                            .body(Response.errorRes("User not found", null));
                }
            } catch (Exception e) {
                throw new Exception("Failed to update user information");
            }
        }
    }

    public ResponseEntity<Object> deleteUserById(int id) throws Exception {
        try {
            Optional<User> user = userRepository.findById(id);
            if (user.isPresent()) {
                userRepository.deleteById(id);
                return ResponseEntity.ok(Response.successRes("User deleted successfully", "No data"));
            } else {
                return ResponseEntity
                        .status(HttpStatus.NOT_FOUND)
                        .body(Response.errorRes("User not found", null));
            }
        } catch (Exception e) {
            throw new Exception("Failed to delete the user");
        }
    }

    @Override
    public UserDetails loadUserByUsername(String username) throws UsernameNotFoundException {
        User user = userRepository.findUserByEmailOrUsername(username, username);
        if (user != null) {
            return user;
        } else {
            throw new UsernameNotFoundException("User not found");
        }
    }
}
