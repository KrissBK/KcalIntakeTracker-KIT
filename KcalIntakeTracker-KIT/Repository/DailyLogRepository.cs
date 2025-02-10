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

		public ICollection<DailyLog> GetDailyLogs()
		{
			return _context.DailyLogs.OrderBy(d => d.LogId).ToList();
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
    }
}
