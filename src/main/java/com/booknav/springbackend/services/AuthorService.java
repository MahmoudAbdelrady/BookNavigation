package com.booknav.springbackend.services;

import com.booknav.springbackend.dto.AuthorDTO;
import com.booknav.springbackend.entities.Author;
import com.booknav.springbackend.repos.AuthorRepository;
import com.booknav.springbackend.utils.response.Response;
import org.modelmapper.ModelMapper;
import org.modelmapper.TypeMap;
import org.modelmapper.TypeToken;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Service;

import java.lang.reflect.Type;
import java.util.List;
import java.util.Optional;

@Service
public class AuthorService {
    private final AuthorRepository authorRepository;
    private final ModelMapper modelMapper;

    @Autowired
    public AuthorService(AuthorRepository authorRepository, ModelMapper modelMapper) {
        this.authorRepository = authorRepository;
        this.modelMapper = modelMapper;
    }

    public ResponseEntity<Object> getAllAuthors() {
        try {
            List<Author> authors = authorRepository.findAll();
            Type listType = new TypeToken<List<AuthorDTO>>() {
            }.getType();

            TypeMap<Author, AuthorDTO> typeMap = modelMapper.getTypeMap(Author.class, AuthorDTO.class);
            if (typeMap == null) {
                typeMap = modelMapper.createTypeMap(Author.class, AuthorDTO.class);
            }
            typeMap.addMappings(mapping -> mapping.skip(AuthorDTO::setBooks));

            List<AuthorDTO> authorDTOS = modelMapper.map(authors, listType);

            return ResponseEntity.ok(Response.successRes("Authors retrieved successfully", authorDTOS));
        } catch (Exception e) {
            return ResponseEntity
                    .status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body("Failed to retrieve authors");
        }
    }

    public ResponseEntity<Object> getAuthorById(int id) {
        try {
            Optional<Author> author = authorRepository.findById(id);
            if (author.isPresent()) {
                AuthorDTO authorDTO = modelMapper.map(author.get(), AuthorDTO.class);
                return ResponseEntity.ok(Response.successRes("Author retrieved successfully", authorDTO));
            } else {
                return ResponseEntity
                        .status(HttpStatus.NOT_FOUND)
                        .body(Response.errorRes("Author not found", null));
            }
        } catch (Exception e) {
            return ResponseEntity
                    .status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body(Response.errorRes("Failed to retrieve the author", null));
        }
    }

    public ResponseEntity<Object> saveAuthor(AuthorDTO authorDTO, String message) {
        try {
            if (authorDTO.getAuthorId() == null) {
                Author author = modelMapper.map(authorDTO, Author.class);
                author = authorRepository.save(author);
                modelMapper.map(author, authorDTO);
                return ResponseEntity
                        .status(HttpStatus.CREATED)
                        .body(Response.successRes(message, authorDTO));
            } else {
                Optional<Author> author = authorRepository.findById(authorDTO.getAuthorId());
                if (author.isPresent()) {
                    modelMapper.map(authorDTO, author.get());
                    authorRepository.save(author.get());
                    modelMapper.map(author.get(), authorDTO);
                    return ResponseEntity.ok(Response.successRes(message, authorDTO));
                } else {
                    return ResponseEntity
                            .status(HttpStatus.NOT_FOUND)
                            .body(Response.errorRes("Author not found", null));
                }
            }
        } catch (Exception ex) {
            return ResponseEntity
                    .status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body(Response.errorRes("Failed to save the author", null));
        }

    }

    public ResponseEntity<Object> deleteAuthorById(int id) {
        try {
            Optional<Author> author = authorRepository.findById(id);
            if (author.isPresent()) {
                authorRepository.deleteById(id);
                return ResponseEntity
                        .ok(Response.successRes("Author deleted successfully", "No data"));
            } else {
                return ResponseEntity
                        .status(HttpStatus.NOT_FOUND)
                        .body(Response.errorRes("Author not found", null));
            }
        } catch (Exception ex) {
            return ResponseEntity
                    .status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body(Response.errorRes("Failed to delete the author", null));
        }
    }
}
