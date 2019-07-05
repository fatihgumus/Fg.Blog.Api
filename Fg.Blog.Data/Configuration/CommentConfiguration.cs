
using Fg.Blog.Model;
using Fg.Blog.Model.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fg.Blog.Data
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey( c => c.Id );
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Content).IsRequired().HasMaxLength(4000); 
            builder.HasOne(s => s.Blog)
                .WithMany(u => u.Comments)
                .HasForeignKey(b => b.BlogId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(b => b.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}