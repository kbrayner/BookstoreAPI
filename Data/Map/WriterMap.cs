using BookstoreSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookstoreSystem.Data.Map
{
    public class WriterMap : IEntityTypeConfiguration<Writer>
    {
        public void Configure(EntityTypeBuilder<Writer> builder)
        {
            builder.HasKey(writer => writer.Id);
            builder.Property(writer => writer.Name).IsRequired().HasMaxLength(50);
        }
    }
}
