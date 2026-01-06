using HealthManagement_MAUI.Models.Entities;
using HealthManagement_MAUI.Models.Entities.Management;
using HealthManagement_MAUI.Models.Entities.Profile;
using Microsoft.EntityFrameworkCore;

namespace Heathy_Maui.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options) { }

        // DbSet cho các entity
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AccountProfile> Profiles { get; set; }
        public DbSet<HealthProfile> HealthProfiles { get; set; }
        public DbSet<Health> Healths { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Sleep> Sleeps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                    new Role
                    {
                        Id = 1,
                        RoleName = "Admin"
                    },
                    new Role
                    {
                        Id = 2,
                        RoleName = "User"
                    }
                );

            // ===== SEED ADMIN ACCOUNT =====
            var adminPasswordHash = BCrypt.Net.BCrypt.HashPassword("admin");

            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@system.com",
                    Password = adminPasswordHash,
                    RoleId = 1,
                    CreatedAt = DateTime.Now,
                    isDeleted = false
                }
            );

            // Profile liên kết Account
            modelBuilder.Entity<AccountProfile>()
                .HasOne(p => p.Account)
                .WithOne(a => a.AccountProfile)
                .HasForeignKey<AccountProfile>(p => p.AccountId);

            // HealthProfile liên kết Profile
            modelBuilder.Entity<HealthProfile>()
                .HasOne(h => h.AccountProfile)
                .WithOne(p => p.HealthProfile)
                .HasForeignKey<HealthProfile>(h => h.ProfileId);

            // Các bảng HealthManagement, FoodManagement, ExerciseManagement, SleepManagement liên kết Profile
            modelBuilder.Entity<Health>()
                .HasOne(h => h.AccountProfile)
                .WithMany(p => p.HealthManagements)
                .HasForeignKey(h => h.ProfileId);

            modelBuilder.Entity<Food>()
                .HasOne(f => f.AccountProfile)
                .WithMany(p => p.FoodManagements)
                .HasForeignKey(f => f.ProfileId);

            modelBuilder.Entity<Exercise>()
                .HasOne(e => e.AccountProfile)
                .WithMany(p => p.ExerciseManagements)
                .HasForeignKey(e => e.ProfileId);

            modelBuilder.Entity<Sleep>()
                .HasOne(s => s.AccountProfile)
                .WithMany(p => p.SleepManagements)
                .HasForeignKey(s => s.ProfileId);
        }
    }
}
