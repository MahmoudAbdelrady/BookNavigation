namespace aspcorebackend.Models {
    public class Category {
        public int CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public DateTime AddedAt { get; set; }
        public List<Book> Books { get; set; } = [];

        public Category() {
            this.AddedAt = DateTime.Now;
        }
    }
}
