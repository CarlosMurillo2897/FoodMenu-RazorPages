using FoodMenu.DataAccess.Data;
using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using System.Linq;

namespace FoodMenu.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private readonly ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
