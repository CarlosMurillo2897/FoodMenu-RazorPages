using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using FoodMenu.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FoodMenu_RazorPages.Pages.Admin.MenuItems
{
    [BindProperties]
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            MenuItem = new MenuItem();
        }
        public MenuItem MenuItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
        public void OnGet(int? id)
        {
            if(id != null)
            {
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.ID == id);
            }
            CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem(i.Name, i.ID.ToString()));
            FoodTypeList = _unitOfWork.FoodType.GetAll().Select(i => new SelectListItem(i.Name, i.ID.ToString()));
        }

        public async Task<IActionResult> OnPost()
        {
            var files = HttpContext.Request.Form.Files;
            var fileHelper = new FileHelper(_webHostEnvironment.WebRootPath);
            
            if(MenuItem.ID == 0 ) {
                var imagePath = fileHelper.CopyFile(files);
                MenuItem.Image = imagePath;
                _unitOfWork.MenuItem.Add(MenuItem); 
            } else {
                var objFromDB = _unitOfWork.MenuItem.GetFirstOrDefault(u => u.ID == MenuItem.ID);
                
                if(files.Count > 0)
                {
                    fileHelper.RemoveFile(objFromDB.Image);
                    var imagePath = fileHelper.CopyFile(files);
                    objFromDB.Image = imagePath;
                } else
                {
                    MenuItem.Image = objFromDB.Image;
                }

                _unitOfWork.MenuItem.Update(objFromDB);
            }
            _unitOfWork.Save();
            
            return RedirectToPage("./Index");
        }
    }
}
