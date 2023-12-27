using FoodMenu.DataAccess.Data;
using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FoodMenu_RazorPages.Pages.Admin.FoodTypes
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public FoodType FoodType { get; set; }
        public void OnGet(int id)
        {
            FoodType = _unitOfWork.FoodType.GetFirstOrDefault(u => u.ID == id);
        }

        public async Task<IActionResult> OnPost()
        {
            var foodTypeFromDB = _unitOfWork.FoodType.GetFirstOrDefault(u => u.ID == FoodType.ID);
            if (foodTypeFromDB != null)
            {
                _unitOfWork.FoodType.Remove(foodTypeFromDB);
                _unitOfWork.Save();
                TempData["success"] = "Food Type deleted successfully.";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
