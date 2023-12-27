using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodMenu_RazorPages.Pages.Admin.MenuItems
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            MenuItem = new MenuItem();
        }
        public MenuItem MenuItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
        public void OnGet()
        {
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem(i.Name, i.ID.ToString()));
            FoodTypeList = _unitOfWork.FoodType.GetAll().Select(i => new SelectListItem(i.Name, i.ID.ToString()));
        }

        public async Task<IActionResult> OnPost()
        {
            return Page();
        }
    }
}
