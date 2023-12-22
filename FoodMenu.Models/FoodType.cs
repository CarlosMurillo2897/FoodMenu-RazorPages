using System.ComponentModel.DataAnnotations;

namespace FoodMenu.Models
{
    public class FoodType
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
