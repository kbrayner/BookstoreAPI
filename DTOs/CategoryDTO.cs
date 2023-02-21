using System.ComponentModel.DataAnnotations;

namespace BookstoreSystem.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The category name must have a maximum of 100 characters")]
        public string Name { get; set; }
    }
}
