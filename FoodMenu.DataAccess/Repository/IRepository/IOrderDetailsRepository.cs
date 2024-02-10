using FoodMenu.Models;

namespace FoodMenu.DataAccess.Repository.IRepository
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {
        void Update(OrderDetails obj);
    }
}
