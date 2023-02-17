using Microsoft.EntityFrameworkCore;
using BugsTrackingSystem.Models;

namespace BugsTrackingSystem.DAL
{
    public class BugsResolvingContext : DbContext
    {

        public BugsResolvingContext(DbContextOptions<BugsResolvingContext> options) : base(options)
        {

        }

        public DbSet<Bug> Bugs { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bug>()
                .ToTable(nameof(Bugs));

            modelBuilder.Entity<Message>()
                .ToTable(nameof(Messages));

            modelBuilder.Entity<Project>()
                .ToTable(nameof(Projects));

            modelBuilder.Entity<User>()
               .ToTable(nameof(Users));

            modelBuilder.Entity<Role>()
               .ToTable(nameof(Roles));

            modelBuilder.Entity<UserRole>()
                .ToTable(nameof(UserRoles))
                .HasKey(ur => new { ur.UserId, ur.RoleId });

        }
    }
}
