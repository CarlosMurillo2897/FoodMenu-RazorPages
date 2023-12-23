using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FoodMenu_RazorPages.Pages.Admin.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ICategoryRepository _dbCategory;
        public CreateModel(ICategoryRepository dbCategory)
        {
            _dbCategory = dbCategory;
        }
        public Category Category { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(Category.Name == Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Category.Name", "The Display Order cannot exactly match the Name.");
            }
            if(ModelState.IsValid)
            {
                _dbCategory.Add(Category);
                TempData["success"] = "Category created successfully.";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
