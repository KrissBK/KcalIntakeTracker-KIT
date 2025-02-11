using KcalIntakeTracker_KIT.Data;
using KcalIntakeTracker_KIT.Interfaces;
using KcalIntakeTracker_KIT.Models;

namespace KcalIntakeTracker_KIT.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly KITDbContext _context;

        public UserRepository(KITDbContext context)
        {
            _context = context;
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.OrderBy(u => u.UserId).ToList();
        }

        public User GetUser(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(u => u.UserId == userId);
        }


    }
}
