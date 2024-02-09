using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;

namespace FoodMenu_RazorPages.Pages.Customer.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            }
        }
    }
}
