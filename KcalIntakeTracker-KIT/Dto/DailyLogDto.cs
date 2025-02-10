namespace KcalIntakeTracker_KIT.Dto
{
    public class DailyLogDto
    {
        public int LogId { get; set; }
        public DateTime LogDate { get; set; }
        public double TotalCalories { get; set; }
        public string Username { get; set; }
    }

}
