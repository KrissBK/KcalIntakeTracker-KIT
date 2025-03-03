using KcalIntakeTracker_KIT.Dto;
using KcalIntakeTracker_KIT.Models;

namespace KcalIntakeTracker_KIT.Interfaces
{
    public interface IDailyLogRepository
    {
        ICollection<DailyLog> GetDailyLogs();
        IQueryable<DailyLog> DailyLogs();
        ICollection<DailyLog> GetDailyLogsByUser(int userId);
        bool DailyLogExists(int logId);

        bool CreateDailyLog(DailyLog dailyLog);

        bool UpdateDailyLog(DailyLog dailyLog);

        bool DeleteDailyLog(DailyLog dailyLog);

        

    }

}
