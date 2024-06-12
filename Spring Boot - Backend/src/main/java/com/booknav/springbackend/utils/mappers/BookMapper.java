package com.booknav.springbackend.utils.mappers;

import com.booknav.springbackend.dto.Book.BookCatReqDTO;
import com.booknav.springbackend.dto.Book.BookDTO;
import com.booknav.springbackend.entities.Book;
import com.booknav.springbackend.entities.Category;
import org.modelmapper.ModelMapper;
import org.modelmapper.convention.MatchingStrategies;


public class BookMapper {
    private final ModelMapper modelMapper;

    public BookMapper() {
        this.modelMapper = new ModelMapper();
        this.modelMapper
                .getConfiguration()
                .setMatchingStrategy(MatchingStrategies.STRICT)
                .setSkipNullEnabled(true);
    }

    public BookDTO convertToDTO(Book book) {
        BookDTO bookDTO = modelMapper.map(book, BookDTO.class);
        if (book.getCategory() != null) {
            BookCatReqDTO bookCatReqDTO = new BookCatReqDTO();
            bookCatReqDTO.setCatId(book.getCategory().getCategoryId());
            bookCatReqDTO.setCatTitle(book.getCategory().getTitle());
            bookDTO.setBookCategory(bookCatReqDTO);
        }
        if (book.getAuthor() != null) {
            bookDTO.setBookAuthor(book.getAuthor().getFirstName() + " " + book.getAuthor().getLastName());
        }
        return bookDTO;
    }
}
