using System.ComponentModel.DataAnnotations;

namespace BookstoreSystem.DTOs
{
    public class WriterDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The writer name must have a maximum of 50 characters")]
        public string Name { get; set; }

    }
}
