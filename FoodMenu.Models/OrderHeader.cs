using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodMenu.Models
{
    public class OrderHeader
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }
        [Required]
        [Display(Name = "Pick Up Time")]
        public DateTime PickupTime { get; set; }
        [Required]
        [NotMapped]
        public DateTime PickupDate { get; set; }
        public string Status { get; set; }
        public string? Comments { get; set; }
        public string? TransactionID { get; set; }
        [Display(Name = "Pickup Name")]
        public string PickupName { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
