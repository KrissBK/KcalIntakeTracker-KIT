using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KcalIntakeTracker_KIT.Models
{
    public class DailyLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public DateTime LogDate { get; set; }
        public double TotalProtein { get; set; }
        public double TotalFat { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalCalories { get; set; }

    }
}
