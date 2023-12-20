using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FoodMenu.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Display order must be in range of 1-100.")]
        public int DisplayOrder { get; set; }
    }
}
