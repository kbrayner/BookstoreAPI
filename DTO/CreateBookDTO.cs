using BookstoreSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace BookstoreSystem.DTO
{
    public class CreateBookDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "The title must have a maximum of 100 characters")]
        public string Title { get; set; }

        [StringLength(100, ErrorMessage = "The subtitle must have a maximum of 100 characters")]
        public string? Subtitle { get; set; }

        [StringLength(500, ErrorMessage = "The resume must have a maximum of 500 characters")]
        public string? Resume { get; set; }

        [Required]
        [Range(1, 10000, ErrorMessage = "The number of pages must be a value between 1 and 10000")]
        public int PagesNumber { get; set; }

        [Required]
        public DateOnly ReleaseDate { get; set; }

        [Range(1, 20, ErrorMessage = "The edition must be a value between 1 and 20")]
        public int? Edition { get; set; }

        [Required]
        public List<Writer> Writer { get; set; }

        [Required]
        public Publisher Publisher { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}
