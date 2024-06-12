using aspcorebackend.Dtos;
using aspcorebackend.Dtos.User;
using aspcorebackend.Models;
using AutoMapper;

namespace aspcorebackend.Utils {
    public class AppMapper : Profile {
        public AppMapper() {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Author, AuthorDTO>().ReverseMap();
            CreateMap<Book, BookDTO>()
                .ForMember(
                dest => dest.BookAuthor,
                opt => opt.MapFrom(src => src.Author != null ? $"{src.Author.Firstname} {src.Author.Lastname}" : null)
                )
                .ForMember(dest => dest.BookCategory, opt => opt.MapFrom(src => src.Category != null ? src.Category.Title : null))
                .ReverseMap();

            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<RegisterDTO, User>().ReverseMap();
        }
    }
}
