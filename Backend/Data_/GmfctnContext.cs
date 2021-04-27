using Data_.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data_
{
    public class GmfctnContext : DbContext
    {
        public GmfctnContext(DbContextOptions options)
           : base(options)
        {

        }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<RequestAchievement> RequestAchievements { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RTokens { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<File> Files { get; set; }
        public DbSet<Thank> Thanks { get; set; }
        public DbSet<PasswordResetRequest> PasswordResetRequests{ get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achievement>().ToTable("Achievements");
            modelBuilder.Entity<Achievement>()
                .HasIndex(u => u.Name)
                .IsUnique();


            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>()
               .HasIndex(u => u.Email)
               .IsUnique();
            modelBuilder.Entity<User>()
               .HasIndex(u => u.UserName)
               .IsUnique();

            modelBuilder.Entity<UserAchievement>()
            .HasKey(bc => new { bc.UserId, bc.AchievementId, bc.Id});
                modelBuilder.Entity<UserAchievement>()
                    .HasOne(bc => bc.User)
                    .WithMany(b => b.UserAchievements)
                    .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<UserAchievement>()
                .HasOne(bc => bc.Achievement)
                .WithMany(c => c.UserAchievements)
                .HasForeignKey(bc => bc.AchievementId);

            modelBuilder.Entity<UserRole>()
            .HasKey(bc => new { bc.UserId, bc.RoleId });
            modelBuilder.Entity<UserRole>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserRoles)
                .HasForeignKey(bc => bc.UserId);
            modelBuilder.Entity<UserRole>()
                .HasOne(bc => bc.Role)
                .WithMany(c => c.UserRoles)
                .HasForeignKey(bc => bc.RoleId); 

            modelBuilder.Entity<RequestAchievement>().HasKey(item => item.Id);

            modelBuilder.Entity<Event>().HasKey(item => item.Id);
            modelBuilder.Entity<Event>().HasOne(Event => Event.User).WithMany(User => User.Events).HasForeignKey(Event => Event.UserId);

            modelBuilder.Entity<File>().ToTable("Files");
            modelBuilder.Entity<PasswordResetRequest>().ToTable("PasswordResetRequests");

        }
    }
}
