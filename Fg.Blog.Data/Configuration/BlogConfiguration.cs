using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fg.Blog.Model
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(b => b.Id);
            
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(b => b.Title)
                .HasMaxLength(100);

            builder.Property(s => s.OwnerId)
                .IsRequired();

            builder.HasOne(s => s.Owner)
                .WithMany(u => u.Blogs)
                .HasForeignKey(b => b.OwnerId);

            builder.HasMany(s => s.Comments)
               .WithOne(u => u.Blog)
               .HasForeignKey(b => b.BlogId);
        }
    }
}