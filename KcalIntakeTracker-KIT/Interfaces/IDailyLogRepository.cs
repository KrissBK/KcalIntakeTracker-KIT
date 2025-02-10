using KcalIntakeTracker_KIT.Dto;
using KcalIntakeTracker_KIT.Models;

namespace KcalIntakeTracker_KIT.Interfaces
{
    public interface IDailyLogRepository
    {
        ICollection<DailyLog> GetDailyLogs();
        IQueryable<DailyLog> DailyLogs();
        ICollection<DailyLogDto> GetDailyLogsByUserId(int userId);

    }

}
