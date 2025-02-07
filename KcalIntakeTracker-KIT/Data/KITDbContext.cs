using KcalIntakeTracker_KIT.Models;
using Microsoft.EntityFrameworkCore;

namespace KcalIntakeTracker_KIT.Data
{

    public class KITDbContext : DbContext
    {
        public KITDbContext(DbContextOptions<KITDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<DailyLog> DailyLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyLog>()
                .HasKey(d => d.LogId);

            modelBuilder.Entity<DailyLog>()
                .HasOne(d => d.User)
                .WithMany(u => u.DailyLogs)
                .HasForeignKey(d => d.UserId);

            //modelBuilder.Entity<User>().ToTable("User");
            //modelBuilder.Entity<FoodItem>().ToTable("FoodItem");
            //modelBuilder.Entity<DailyLog>().ToTable("DailyLog");
        }
    }
}
