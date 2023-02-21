using System.ComponentModel.DataAnnotations;

namespace BookstoreSystem.DTO
{
    public class WriterDTO
    {
        [Required]
        [StringLength(50, ErrorMessage = "The writer name must have a maximum of 50 characters")]
        public string Name { get; set; }

    }
}
