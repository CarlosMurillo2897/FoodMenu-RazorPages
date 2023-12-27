using FoodMenu.DataAccess.Data;
using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using System.Linq;

namespace FoodMenu.DataAccess.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _db;
        public MenuItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(MenuItem obj)
        {
            var objFromDB = _db.MenuItem.FirstOrDefault(u => u.ID == obj.ID);
            objFromDB.Name = obj.Name;
            objFromDB.Description = obj.Description;
            objFromDB.Price = obj.Price;
            objFromDB.CategoryID = obj.CategoryID;
            objFromDB.FoodTypeID = obj.FoodTypeID;
            if(objFromDB.Image != null)
            {
                objFromDB.Image = obj.Image;
            }
        }
    }
}
