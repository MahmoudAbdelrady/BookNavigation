namespace aspcorebackend.Models {
    public class Book {
        public int BookId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int PublishYear { get; set; }
        public string Cover { get; set; } = null!;
        public decimal Price { get; set; }
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
        public DateTime AddedAt { get; set; }
        public Author? Author { get; set; }
        public Category? Category { get; set; }

        public Book() {
            this.AddedAt = DateTime.Now;
        }
    }
}
