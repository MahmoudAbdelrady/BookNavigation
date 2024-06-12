namespace aspcorebackend.Models {
    public class Author {
        public int AuthorId { get; set; }
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public DateTime AddedAt { get; set; }
        public List<Book> Books { get; set; } = [];

        public Author() {
            this.AddedAt = DateTime.Now;
        }
    }
}
