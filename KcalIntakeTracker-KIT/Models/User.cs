using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KcalIntakeTracker_KIT.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }  // Primary Key
        public required string Username { get; set; } 
        public required string PasswordHash { get; set; }
        public required string Email { get; set; }
        public bool EmailVerified { get; set; } = false;
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
