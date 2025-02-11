using KcalIntakeTracker_KIT.Data;
using KcalIntakeTracker_KIT.Models;
using KcalIntakeTracker_KIT.Interfaces;
using Microsoft.EntityFrameworkCore;
using KcalIntakeTracker_KIT.Dto;

namespace KcalIntakeTracker_KIT.Repository
{
    public class FoodItemRepository : IFoodItemRepository
    {
        private readonly KITDbContext _context;

        public FoodItemRepository(KITDbContext context)
        {
            _context = context;
        }

        public ICollection<FoodItemDto> GetFoodItems()
        {
            return _context.FoodItems
                .Include(f => f.User)
                .Select(f => new FoodItemDto
                {
                    FoodItemId = f.FoodItemId,
                    FoodName = f.FoodName,
                    Protein = f.Protein,
                    Fat = f.Fat,
                    Carbohydrates = f.Carbohydrates,
                    Calories = f.Calories,
                    Username = f.User.Username
                })
                .OrderBy(f => f.FoodItemId)
                .ToList();
        }



        public ICollection<FoodItemDto> GetFoodItemsByUserId(int userId)
        {
            return _context.FoodItems
                .Where(f => f.UserId == userId)
                .Include(f => f.User)
                .Select(f => new FoodItemDto
                {
                    FoodItemId = f.FoodItemId,
                    FoodName = f.FoodName,
                    Protein = f.Protein,
                    Fat = f.Fat,
                    Carbohydrates = f.Carbohydrates,
                    Calories = f.Calories,
                    Username = f.User.Username
                })
                .ToList();
        }


        public FoodItemDto GetFoodItem(int foodItemId)
        {
            return _context.FoodItems
                .Where(f => f.FoodItemId == foodItemId)
                .Include(f => f.User)
                .Select(f => new FoodItemDto
                {
                    FoodItemId = f.FoodItemId,
                    FoodName = f.FoodName,
                    Protein = f.Protein,
                    Fat = f.Fat,
                    Carbohydrates = f.Carbohydrates,
                    Calories = f.Calories,
                    Username = f.User.Username ?? string.Empty
                })
                .FirstOrDefault() ?? new FoodItemDto();
        }
    }
}
