using FoodMenu.DataAccess.Data;
using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FoodMenu_RazorPages.Pages.Admin.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Category Category { get; set; }
        public void OnGet(int id)
        {
            Category = _unitOfWork.Category.GetFirstOrDefault(u => u.ID == id);
        }

        public async Task<IActionResult> OnPost()
        {
            var categoryFromDB = _unitOfWork.Category.GetFirstOrDefault(u => u.ID == Category.ID);
            if (categoryFromDB != null)
            {
                _unitOfWork.Category.Remove(categoryFromDB);
                _unitOfWork.Save();
                TempData["success"] = "Category deleted successfully.";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
