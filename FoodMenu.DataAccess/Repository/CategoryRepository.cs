﻿using FoodMenu.DataAccess.Data;
using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using System.Linq;

namespace FoodMenu.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            var objFromDB = _db.Category.FirstOrDefault(u => u.ID == obj.ID);
            objFromDB.Name = obj.Name;
            objFromDB.DisplayOrder = obj.DisplayOrder;
        }
    }
}
