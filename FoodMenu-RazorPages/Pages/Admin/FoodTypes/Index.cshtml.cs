using FoodMenu.DataAccess.Data;
using FoodMenu.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace FoodMenu_RazorPages.Pages.Admin.FoodTypes
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public IEnumerable<FoodType> FoodTypes { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            FoodTypes = _db.FoodType;
        }
    }
}
