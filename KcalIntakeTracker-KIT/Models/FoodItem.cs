namespace KcalIntakeTracker_KIT.Models
{
    public class FoodItem
    {
        public int FoodItemId { get; set; }  // Primary Key
        public string Name { get; set; } = string.Empty;
        public double Protein { get; set; }  // grams
        public double Fat { get; set; }  // grams
        public double Carbs { get; set; }  // grams
        public double Calories { get; set; }  // kcal
    }

}
