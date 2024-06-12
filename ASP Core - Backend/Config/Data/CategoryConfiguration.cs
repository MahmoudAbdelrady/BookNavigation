using aspcorebackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aspcorebackend.Config.Data {
    public class CategoryConfiguration : IEntityTypeConfiguration<Category> {
        public void Configure(EntityTypeBuilder<Category> builder) {
            builder.HasKey(c => c.CategoryId);
            builder.Property(c => c.CategoryId).ValueGeneratedOnAdd();

            builder.Property(c => c.Title).HasColumnType("VARCHAR").HasMaxLength(150).IsRequired();

            builder.ToTable("categories");
        }
    }
}
