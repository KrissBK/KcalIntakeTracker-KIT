using KcalIntakeTracker_KIT.Dto;
using KcalIntakeTracker_KIT.Models;

namespace KcalIntakeTracker_KIT.Interfaces
{
    public interface IFoodItemRepository
    {
        ICollection<FoodItemDto> GetFoodItems();
        ICollection<FoodItemDto> GetFoodItemsByUserId(int userId);
       FoodItemDto  GetFoodItem(int FoodItemId);
    }
}
