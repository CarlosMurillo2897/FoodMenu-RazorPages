using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodMenu.Models
{
    public class OrderDetails
    {
        public int ID { get; set; }
        [Required]
        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public OrderHeader OrderHeader { get; set; }
        [Required]
        public int MenuItemID { get; set; }
        [ForeignKey("MenuItemID")]
        public virtual MenuItem MenuItem { get; set; }
        public int Count { get; set; }
        [Required]
        public double Price { get; set; }
        public string Name { get; set; }
    }
}
