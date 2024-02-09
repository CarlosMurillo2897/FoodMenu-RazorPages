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
    public class IndexModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        public double CartTotal { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartTotal = 0;
        }
        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if(claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(
                    filter: x => x.ApplicationUserID == claim.Value, 
                    includeProperties: new[] { nameof(MenuItem), $"{nameof(MenuItem)}.{nameof(FoodType)}", $"{nameof(MenuItem)}.{nameof(Category)}" }
                );
                foreach (var cartItem in ShoppingCartList)
                {
                    CartTotal += (cartItem.MenuItem.Price * cartItem.Count);
                }
            }
        }

        public IActionResult OnPostPlus(int cartID)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(x => x.ID == cartID);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            _unitOfWork.Save();
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostMinus(int cartID)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(x => x.ID == cartID);
            if(cart.Count == 1)
            {
                _unitOfWork.ShoppingCart.Remove(cart);
            } else {
                _unitOfWork.ShoppingCart.DecrementCount(cart, 1);
            }
            _unitOfWork.Save();
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostRemove(int cartID)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(x => x.ID == cartID);
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            return RedirectToPage("/Customer/Cart/Index");
        }

    }
}
