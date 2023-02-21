namespace BookstoreSystem.Models
{
    public class Writer
    {
        public Writer()
        {
            this.Books = new HashSet<Book>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
