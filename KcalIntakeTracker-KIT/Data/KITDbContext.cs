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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
                //entity.Property(e => e.Weight).IsRequired();
                //entity.Property(e => e.FatPercentage).IsRequired();
            

            modelBuilder.Entity<FoodItem>()
                .HasKey(f => f.FoodItemId);

            modelBuilder.Entity<DailyLog>()
                .HasKey(d => d.LogId);

            modelBuilder.Entity<DailyLog>()
                .HasOne(d => d.User)
                .WithMany(u => u.DailyLogs)
                .HasForeignKey(d => d.UserId);

            modelBuilder.Entity<FoodItem>()
                .HasOne(f => f.User)
                .WithMany(u => u.FoodItems)
                .HasForeignKey(f => f.UserId);
        }
    }
}