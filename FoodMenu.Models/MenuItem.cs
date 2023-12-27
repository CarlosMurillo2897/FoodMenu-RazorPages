using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodMenu.Models
{
    public class MenuItem
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        [Range(0, 1000, ErrorMessage = "Price should be between $1 and $1000.")]
        public double Price { get; set; }
        public int FoodTypeID { get; set; }
        public FoodType FoodType { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
