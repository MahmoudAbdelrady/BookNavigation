using System.ComponentModel.DataAnnotations;

namespace aspcorebackend.Dtos {
    public class BookDTO {
        public int BookId { get; set; }

        [Required(ErrorMessage = "Book title is required")]
        [MaxLength(150, ErrorMessage = "Book title can't exceed 150 characters.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Book description is required")]
        [MaxLength(300, ErrorMessage = "Book description can't exceed 300 characters.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Book publish year is required")]
        [RegularExpression(@"\b(1[0-9]{3}|[2-9][0-9]{3})\b", ErrorMessage = "Please enter a valid year")]
        public int PublishYear { get; set; }

        [Required(ErrorMessage = "Book cover's image link is required")]
        public string Cover { get; set; } = null!;

        [Required(ErrorMessage = "Book price is required")]
        [Range(1, 3000, ErrorMessage = "Book price must be between 1 and 3000")]
        public decimal Price { get; set; }

        public string? BookAuthor { get; set; }

        public string? BookCategory { get; set; }
    }
}
