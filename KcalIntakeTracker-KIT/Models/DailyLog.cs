namespace KcalIntakeTracker_KIT.Models
{
    public class DailyLog
    {
        public int DailyLogId { get; set; }  // Primary Key
        public DateTime Date { get; set; }

        public int UserId { get; set; }  // Foreign Key
        public User User { get; set; }

        public double TotalProtein { get; set; }
        public double TotalFat { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalCalories { get; set; }
    }

}
