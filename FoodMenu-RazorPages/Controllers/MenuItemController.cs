using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodMenu_RazorPages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MenuItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var menuItemList = _unitOfWork.MenuItem.GetAll(nameof(Category), nameof(FoodType));
            return Json(new
            {
                data = menuItemList
            });
        }
    }
}
