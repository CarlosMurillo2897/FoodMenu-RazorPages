using FoodMenu.DataAccess.Data;
using FoodMenu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FoodMenu_RazorPages.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public FoodType FoodType { get; set; }
        public void OnGet(int id)
        {
            FoodType = _db.FoodType.Find(id);
        }

        public async Task<IActionResult> OnPost()
        {
            var foodTypeFromDB = _db.FoodType.Find(FoodType.ID);
            if(foodTypeFromDB != null)
            {
                _db.FoodType.Remove(foodTypeFromDB);
                await _db.SaveChangesAsync();
                TempData["success"] = "Food Type deleted successfully.";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
