using System.ComponentModel.DataAnnotations;

namespace BookstoreSystem.DTO
{
    public class PublisherDTO
    {
        [Required]
        [StringLength(150, ErrorMessage = "The publisher name must have a maximum of 150 characters")]
        public string Name { get; set; }
    }
}
