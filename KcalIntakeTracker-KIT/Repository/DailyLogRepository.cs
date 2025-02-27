using KcalIntakeTracker_KIT.Data;
using KcalIntakeTracker_KIT.Models;
using KcalIntakeTracker_KIT.Interfaces;
using Microsoft.EntityFrameworkCore;
using KcalIntakeTracker_KIT.Dto;

namespace KcalIntakeTracker_KIT.Repository
{
	public class DailyLogRepository : IDailyLogRepository
	{
		private readonly KITDbContext _context;

		public DailyLogRepository(KITDbContext context)
		{
			_context = context;
		}

		public ICollection<DailyLogDto> GetDailyLogs()
		{
            return _context.DailyLogs
                .Include(d => d.User)
                .Select(d => new DailyLogDto
                {
                    LogId = d.LogId,
                    LogDate = d.LogDate,
                    TotalProtein = d.TotalProtein,
                    TotalFat = d.TotalFat,
                    TotalCarbs = d.TotalCarbs,
                    TotalCalories = d.TotalCalories,
                    Weight = d.User.Weight,
                    FatPercentage = d.User.FatPercentage,
                    Username = d.User.Username
                })
                .OrderBy(d => d.LogId)
                .ToList();
        }

		public IQueryable<DailyLog> DailyLogs()
		{
			return _context.DailyLogs.AsQueryable();
		}

        public ICollection<DailyLogDto> GetDailyLogsByUserId(int userId)
        {
            return _context.DailyLogs
                .Where(d => d.UserId == userId)
                .Include(d => d.User)
                .Select(d => new DailyLogDto
                {
                    LogId = d.LogId,
                    LogDate = d.LogDate,
                    TotalCalories = d.TotalCalories,
                    Username = d.User.Username
                })
                .ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool DailyLogExists(int logId)
        {
            return _context.DailyLogs.Any(d => d.LogId == logId);
        }

        public bool CreateDailyLog(DailyLog dailyLog)
        {
            var existingUser = _context.Users.Local.FirstOrDefault(u => u.UserId == dailyLog.User.UserId);
            if (existingUser != null) // if user is already in context, detach it
            {
                _context.Entry(existingUser).State = EntityState.Detached;
            }
            _context.Attach(dailyLog.User);

            dailyLog.User.UpdatedAt = DateTime.UtcNow;

            _context.Add(dailyLog);
            return Save();
        }


        public bool UpdateDailyLog(DailyLog dailyLog)
        {
            _context.Update(dailyLog);
            return Save();
        }

        public bool DeleteDailyLog(DailyLog dailyLog)
        {
            _context.Remove(dailyLog);
            return Save();
        }
    }
}
