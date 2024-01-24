using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace FoodMenu_RazorPages.Pages.Customer.Home
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }
        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCart = new ShoppingCart()
            {
                ApplicationUserID = claim.Value,
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(x => x.ID == id, includeProperties: new[] { nameof(Category), nameof(FoodType) }),
                MenuItemID = id
            };
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                ShoppingCart shoppingCartFromDB = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                    x => x.ApplicationUserID == ShoppingCart.ApplicationUserID && x.MenuItemID == ShoppingCart.MenuItemID
                );

                if(shoppingCartFromDB == null ) {
                    _unitOfWork.ShoppingCart.Add(ShoppingCart);
                } else
                {
                    _unitOfWork.ShoppingCart.IncrementCount(shoppingCartFromDB, ShoppingCart.Count);
                }
                _unitOfWork.Save();

                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
