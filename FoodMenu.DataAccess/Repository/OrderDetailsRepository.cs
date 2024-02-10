using FoodMenu.DataAccess.Data;
using FoodMenu.DataAccess.Repository.IRepository;
using FoodMenu.Models;
using System.Linq;

namespace FoodMenu.DataAccess.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetails obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
