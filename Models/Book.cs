using System.Collections.Generic;

namespace BookstoreSystem.Models
{
    public class Book
    {
        public Book()
        {
            this.Writers = new HashSet<Writer>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public string? Subtitle { get; set; }

        public string? Resume { get; set; }

        public int PagesNumber { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public int Edition { get; set; }

        public virtual ICollection<Writer> Writers { get; set; }

        public int PublisherId { get; set; }

        public Publisher Publisher { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
