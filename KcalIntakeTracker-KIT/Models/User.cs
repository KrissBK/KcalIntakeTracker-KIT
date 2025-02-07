using System.Text.Json.Serialization;

namespace KcalIntakeTracker_KIT.Models
{
    public class User
    {
        public int UserId { get; set; }  // Primary Key
        public string Username { get; set; } = string.Empty;
        public double Weight { get; set; }  // kg
        public double FatPercentage { get; set; }  // %
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation Property
        [JsonIgnore]
        public List<DailyLog> DailyLogs { get; set; } = new();

        public List<FoodItem> FoodItems { get; set; } = new();
    }

}
