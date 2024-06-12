using System.ComponentModel.DataAnnotations;

namespace aspcorebackend.Dtos {
    public class AuthorDTO {
        public int AuthorId { get; set; }
        [MaxLength(150, ErrorMessage = "Author's firstname can't exceed 150 characters")]
        [Required(ErrorMessage = "Author's firstname is required")]
        public string Firstname { get; set; } = null!;
        [MaxLength(150, ErrorMessage = "Author's lastname can't exceed 150 characters")]
        [Required(ErrorMessage = "Author's lastname is required")]
        public string Lastname { get; set; } = null!;
        public DateTime AddedAt { get; set; }
        public List<BookDTO> Books { get; set; } = [];

        public AuthorDTO() {
            AddedAt = DateTime.Now;
        }
    }
}
