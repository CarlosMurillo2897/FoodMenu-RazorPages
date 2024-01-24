using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodMenu.Models
{
    public class ShoppingCart
    {
        public int ID { get; set; }
        public int MenuItemID { get; set; }
        [ForeignKey("MenuItemID")]
        [NotMapped]
        [ValidateNever]
        public MenuItem MenuItem { get; set; }
        [Range(1, 100, ErrorMessage = "Please select a count between 1 and 100.")]
        public int Count { get; set; }
        public string ApplicationUserID { get; set; }
        [ForeignKey("ApplicationUserID")]
        [NotMapped]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
