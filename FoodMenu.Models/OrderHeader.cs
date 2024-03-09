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
        [Display(Name = "Pick Up Date")]
        public DateTime PickupDate { get; set; }
        public string Status { get; set; }
        public string? Comments { get; set; }
        public string? SessionID { get; set; }
        public string? PaymentIntentID { get; set; }
        [Display(Name = "Pickup Name")]
        [Required]
        public string PickupName { get; set; }
        [Display(Name = "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
