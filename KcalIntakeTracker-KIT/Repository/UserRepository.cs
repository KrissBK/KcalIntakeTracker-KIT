using KcalIntakeTracker_KIT.Data;
using KcalIntakeTracker_KIT.Interfaces;
using KcalIntakeTracker_KIT.Models;
using KcalIntakeTracker_KIT.Dto;
using Microsoft.EntityFrameworkCore;

namespace KcalIntakeTracker_KIT.Repository
{
	public class UserRepository : IUserRepository
	{
		private readonly KITDbContext _context;

		public UserRepository(KITDbContext context)
		{
			_context = context;
		}

		public ICollection<UserDto> GetUsers()
		{
			return _context.Users
                .Select(u => new UserDto
                {
                    UserId = u.UserId,
                    Username = u.Username,
                    //Email = u.Email,
                    PasswordHash = u.PasswordHash, // dette føles veldig feil..
                    Weight = u.Weight,
                    FatPercentage = u.FatPercentage
                })
                .OrderBy(u => u.UserId)
                .ToList();
        }

        public User GetUser(int userId)
        {
            return _context.Users
                .FirstOrDefault(u => u.UserId == userId);
        }



        public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}

		public bool UserExists(int userId)
		{
			return _context.Users.Any(u => u.UserId == userId);
		}

		public bool CreateUser(User user)
		{
			_context.Add(user);
			return Save();
		}

		public bool UpdateUser(User user)
		{
			_context.Update(user);
			return Save();
		}

		public bool DeleteUser(User userId)
		{
			_context.Remove(userId);
			return Save();
		}


	}
}
