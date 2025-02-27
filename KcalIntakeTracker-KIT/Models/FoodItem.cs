using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KcalIntakeTracker_KIT.Models
{
    public class FoodItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FoodItemId { get; set; }  // Primary Key
        public int UserId { get; set; }  // Foreign Key

        public required User User { get; set; } // Navigation Property
        public string FoodName { get; set; } = string.Empty;
        public double Protein { get; set; }  // grams
        public double Fat { get; set; }  // grams
        public double Carbohydrates { get; set; }  // grams
        public double Calories { get; set; }  // kcal
    }

}
