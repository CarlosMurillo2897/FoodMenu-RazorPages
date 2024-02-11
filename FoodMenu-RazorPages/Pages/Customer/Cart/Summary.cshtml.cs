using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;

namespace FoodMenu_RazorPages.Pages.Customer.Cart
{
    [Authorize]
    public class SummaryModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public OrderHeader OrderHeader { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public SummaryModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            OrderHeader = new OrderHeader();
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
                    filter: x => x.ApplicationUserID == claim.Value,
                    includeProperties: new[] { nameof(MenuItem), $"{nameof(MenuItem)}.{nameof(FoodType)}", $"{nameof(MenuItem)}.{nameof(Category)}" }
                );
                foreach (var cartItem in ShoppingCartList)
                {
                    OrderHeader.OrderTotal += (cartItem.MenuItem.Price * cartItem.Count);
                }
                ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(x => x.Id == claim.Value);
                OrderHeader.PickupName = $"{applicationUser.FirstName} {applicationUser.LastName}";
                OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
            }
        }
    }
}
