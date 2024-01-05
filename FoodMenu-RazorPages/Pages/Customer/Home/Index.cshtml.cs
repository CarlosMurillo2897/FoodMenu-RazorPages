using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace FoodMenu_RazorPages.Pages.Customer.Home
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<MenuItem> MenuItemList { get; set; }
        public IEnumerable<Category> CategoryList { get; set; }

        public void OnGet()
        {
            MenuItemList = _unitOfWork.MenuItem.GetAll(includeProperties: new[] { nameof(Category), nameof(FoodType) });
            CategoryList = _unitOfWork.Category.GetAll(orderBy: x => x.OrderBy(c => c.DisplayOrder) );
        }
    }
}
