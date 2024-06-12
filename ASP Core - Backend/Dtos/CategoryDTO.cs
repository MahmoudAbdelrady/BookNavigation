using System.ComponentModel.DataAnnotations;

namespace aspcorebackend.Dtos {
    public class CategoryDTO {
        public int CategoryId { get; set; }
        [MaxLength(150, ErrorMessage = "Category title can't exceed 150 characters.")]
        [Required(ErrorMessage = "Category title is required")]
        public string Title { get; set; } = null!;
        public DateTime AddedAt { get; set; }
        public List<BookDTO> Books { get; set; } = [];

        public CategoryDTO() {
            AddedAt = DateTime.Now;
        }
    }
}
