using BookstoreSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookstoreSystem.Data.Map
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(book => book.Id);
            builder.Property(book => book.Title).IsRequired().HasMaxLength(100);
            builder.Property(book => book.Subtitle).HasMaxLength(100);
            builder.Property(book => book.Resume).HasMaxLength(500);
            builder.Property(book => book.PagesNumber).IsRequired();
            builder.Property(book => book.ReleaseDate).IsRequired();
            builder.Property(book => book.PublisherId).IsRequired();
            builder.Property(book => book.CategoryId).IsRequired();
        }
    }
}
