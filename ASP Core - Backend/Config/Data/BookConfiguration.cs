using aspcorebackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aspcorebackend.Config.Data {
    public class BookConfiguration : IEntityTypeConfiguration<Book> {
        public void Configure(EntityTypeBuilder<Book> builder) {
            builder.HasKey(b => b.BookId);
            builder.Property(b => b.BookId).ValueGeneratedOnAdd();

            builder.Property(b => b.Title).HasColumnType("VARCHAR").HasMaxLength(150).IsRequired();

            builder.Property(b => b.Description).HasColumnType("VARCHAR").HasMaxLength(300).IsRequired();

            builder.Property(b => b.PublishYear).IsRequired();

            builder.Property(b => b.Cover).HasColumnType("TEXT").IsRequired();

            builder.Property(b => b.Price).HasPrecision(10, 2).IsRequired();

            builder.HasOne(b => b.Author).WithMany(a => a.Books).HasForeignKey(b => b.AuthorId).OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(b => b.Category).WithMany(c => c.Books).HasForeignKey(b => b.CategoryId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
