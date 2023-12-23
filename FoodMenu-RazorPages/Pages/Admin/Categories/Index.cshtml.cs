using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace FoodMenu_RazorPages.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ICategoryRepository _dbCategory;
        public IndexModel(ICategoryRepository dbCategory)
        {
            _dbCategory = dbCategory;
        }
        public IEnumerable<Category> Categories { get; set; }


        public void OnGet()
        {
            Categories = _dbCategory.GetAll();
        }
    }
}
