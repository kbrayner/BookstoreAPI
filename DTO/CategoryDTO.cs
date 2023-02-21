using System.ComponentModel.DataAnnotations;

namespace BookstoreSystem.DTO
{
    public class CategoryDTO
    {
        [Required]
        [StringLength(100, ErrorMessage = "The category name must have a maximum of 100 characters")]
        public string Name { get; set; }
    }
}
