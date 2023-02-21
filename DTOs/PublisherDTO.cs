using System.ComponentModel.DataAnnotations;

namespace BookstoreSystem.DTOs
{
    public class PublisherDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150, ErrorMessage = "The publisher name must have a maximum of 150 characters")]
        public string Name { get; set; }
    }
}
