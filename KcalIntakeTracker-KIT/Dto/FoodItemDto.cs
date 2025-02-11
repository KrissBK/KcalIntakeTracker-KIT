using System.Text.Json.Serialization;

namespace KcalIntakeTracker_KIT.Dto
{
    public class FoodItemDto
    {
        [JsonIgnore]
        public int FoodItemId { get; set; }
        public string FoodName { get; set; } = string.Empty;
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbohydrates { get; set; }
        public double Calories { get; set; }

        [JsonIgnore]
        public string Username { get; set; } = string.Empty;
    }
}
