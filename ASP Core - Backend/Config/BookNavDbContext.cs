using aspcorebackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace aspcorebackend.Config {
    public class BookNavDbContext : IdentityDbContext<User, IdentityRole<int>, int> {
        public BookNavDbContext() { }
        public BookNavDbContext(DbContextOptions<BookNavDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookNavDbContext).Assembly);
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
