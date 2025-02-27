﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KcalIntakeTracker_KIT.Models
{
    public class DailyLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogId { get; set; }  // Primary Key
        public int UserId { get; set; }  // Foreign Key


        public required User User { get; set; } // Navigation Property

        public DateTime LogDate { get; set; } // Date
        public double TotalProtein { get; set; } // grams
        public double TotalFat { get; set; } // grams
        public double TotalCarbs { get; set; } // grams
        public double TotalCalories { get; set; } // kcal


    }
}
