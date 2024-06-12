using aspcorebackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aspcorebackend.Config.Data {
    public class AuthorConfiguration : IEntityTypeConfiguration<Author> {
        public void Configure(EntityTypeBuilder<Author> builder) {
            builder.HasKey(a => a.AuthorId);
            builder.Property(a => a.AuthorId).ValueGeneratedOnAdd();

            builder.Property(a => a.Firstname).HasColumnType("VARCHAR").HasMaxLength(150).IsRequired();

            builder.Property(a => a.Lastname).HasColumnType("VARCHAR").HasMaxLength(150).IsRequired();

            builder.ToTable("authors");
        }
    }
}
