using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
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
            string webRootPath = _webHostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            if(MenuItem.ID == 0 ) {
                string newFileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\MenuItems");
                var extension = Path.GetExtension(files[0].FileName);
                var fullPath = Path.Combine(uploads, newFileName + extension);

                using(var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                MenuItem.Image = $@"\images\MenuItems\{newFileName}{extension}";
                _unitOfWork.MenuItem.Add(MenuItem);
                _unitOfWork.Save();
            } else
            {

            }
            return RedirectToPage("./Index");
        }
    }
}
