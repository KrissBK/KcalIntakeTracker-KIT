using System.ComponentModel.DataAnnotations;

namespace KcalIntakeTracker_KIT.Models
{
    public class DailyLog
    {
        [Key]
        public int LogId { get; set; }  // Primary Key
        public int UserId { get; set; }  // Foreign Key


        public User User { get; set; } // Navigation Property

        public DateTime LogDate { get; set; }
        public double TotalProtein { get; set; }
        public double TotalFat { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalCalories { get; set; }

        
        //public List<User> Users { get; set; } = new();
    }
}
