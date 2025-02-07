using KcalIntakeTracker_KIT.Models;

namespace KcalIntakeTracker_KIT.Interfaces
{
    public interface IDailyLogRepository
    {
        ICollection<DailyLog> GetDailyLogs();
        IQueryable<DailyLog> DailyLogs();
        ICollection<DailyLog> GetDailyLogsByUserId(int userId);

    }

}
