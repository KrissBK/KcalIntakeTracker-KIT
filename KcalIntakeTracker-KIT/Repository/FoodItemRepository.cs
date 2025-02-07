using KcalIntakeTracker_KIT.Data;
using KcalIntakeTracker_KIT.Models;
using KcalIntakeTracker_KIT.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KcalIntakeTracker_KIT.Repository
{
    public class FoodItemRepository : IFoodItemRepository
    {
        private readonly KITDbContext _context;

        public FoodItemRepository(KITDbContext context)
        {
            _context = context;
        }

        public ICollection<FoodItem> GetFoodItems()
        {
            return _context.FoodItems.OrderBy(f => f.FoodItemId).ToList();
        }

        public ICollection<FoodItem> GetFoodItemsByUserId(int userId)
        {
            return _context.FoodItems.Where(f => f.UserId == userId).Include(f => f.User).ToList();
        }

        public IQueryable<FoodItem> GetFoodItem()
        {
            return _context.FoodItems.AsQueryable();
        }
    }
}
