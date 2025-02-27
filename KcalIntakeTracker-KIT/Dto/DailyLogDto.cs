using System.Text.Json.Serialization;

namespace KcalIntakeTracker_KIT.Dto
{
    public class DailyLogDto
    {

        public int LogId { get; set; }
        public DateTime LogDate { get; set; }
        public double TotalProtein { get; set; }
        public double TotalFat { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalCalories { get; set; }
        public double Weight { get; set; }
        public double FatPercentage { get; set; }

        [JsonIgnore]
        public string Username { get; set; } = string.Empty;
    }

}
