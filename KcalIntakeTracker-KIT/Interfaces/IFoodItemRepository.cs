using KcalIntakeTracker_KIT.Models;

namespace KcalIntakeTracker_KIT.Interfaces
{
    public interface IFoodItemRepository
    {
        ICollection<FoodItem> GetFoodItems();
        ICollection<FoodItem> GetFoodItemsByUserId(int userId);
        IQueryable<FoodItem> GetFoodItem();
    }
}
