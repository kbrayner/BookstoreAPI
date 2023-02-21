﻿namespace BookstoreSystem.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Resume { get; set; }

        public int PagesNumber { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public int Edition { get; set; }

        public Book Writer { get; set; }

        public Publisher Publisher { get; set; }

        public Category Category { get; set; }
    }
}
