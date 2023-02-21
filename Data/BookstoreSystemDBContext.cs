using BookstoreSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreSystem.Data
{
    public class BookstoreSystemDBContext : DbContext
    {
        public BookstoreSystemDBContext(DbContextOptions<BookstoreSystemDBContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }

        public DbSet<Writer> Writers { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
