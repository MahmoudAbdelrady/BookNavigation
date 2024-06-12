using aspcorebackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace aspcorebackend.Config.Data {
    public class UserConfiguration : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(u => u.Email).IsRequired();

            builder.Property(u => u.PasswordHash).HasColumnName("Password").IsRequired();

            builder.ToTable("users");
        }
    }
}
