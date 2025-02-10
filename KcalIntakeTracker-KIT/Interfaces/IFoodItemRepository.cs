using KcalIntakeTracker_KIT.Dto;
using KcalIntakeTracker_KIT.Models;

namespace KcalIntakeTracker_KIT.Interfaces
{
    public interface IFoodItemRepository
    {
        ICollection<FoodItem> GetFoodItems();
        ICollection<FoodItemDto> GetFoodItemsByUserId(int userId);
        IQueryable<FoodItem> GetFoodItem();
    }
}
