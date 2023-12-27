using FoodMenu.Models;

namespace FoodMenu.DataAccess.Repository.IRepository
{
    public interface IFoodTypeRepository : IRepository<FoodType>
    {
        void Update(FoodType obj);
    }   
}
