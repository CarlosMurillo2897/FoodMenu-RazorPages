using FoodMenu.DataAccess.Data;
using FoodMenu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FoodMenu_RazorPages.Pages.Admin.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public Category Category { get; set; }
        public void OnGet(int id)
        {
            Category = _db.Category.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var categoryFromDB = _db.Category.Find(Category.ID);
            if(categoryFromDB != null)
            {
                _db.Category.Remove(categoryFromDB);
                await _db.SaveChangesAsync();
                TempData["success"] = "Category deleted successfully.";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
