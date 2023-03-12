using BookstoreSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookstoreSystem.Data.Map
{
    public class PublisherMap : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(publisher => publisher.Id);
            builder.HasIndex(publisher => publisher.Name).IsUnique();
            builder.Property(publisher => publisher.Name).IsRequired().HasMaxLength(150);
            
        }
    }
}
