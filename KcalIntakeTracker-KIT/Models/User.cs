namespace KcalIntakeTracker_KIT.Models
{
    public class User
    {
        public int UserId { get; set; }  // Primary Key
        public string Name { get; set; } = string.Empty;
        public double Weight { get; set; }  // kg
        public double FatPercentage { get; set; }  // %

        // Navigation Property
        public List<DailyLog> DailyLogs { get; set; } = new();
    }

}
