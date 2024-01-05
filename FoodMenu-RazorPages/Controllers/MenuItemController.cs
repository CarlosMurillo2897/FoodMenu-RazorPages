using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using FoodMenu.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FoodMenu_RazorPages.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var menuItemList = _unitOfWork.MenuItem.GetAll(includeProperties: new[] { nameof(Category), nameof(FoodType) });
            return Json(new
            {
                data = menuItemList
            });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDB = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.ID == id);
            if (objFromDB != null)
            {
                var fileHelper = new FileHelper(_webHostEnvironment.WebRootPath);
                fileHelper.RemoveFile(objFromDB.Image);
                _unitOfWork.MenuItem.Remove(objFromDB);
                _unitOfWork.Save();
            }
            return Json(new
            {
                success = true,
                message = "Delete successful."
            });
        }

    }
}
