using GoalCalendar.UserIdentity.Data.Core.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GoalCalendar.UserIdentity.Data.Infrastructure.Database
{
    public class UserDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public UserDbContext()
        {
        }

        public UserDbContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<User>()
                .ToTable("User");
            user.HasKey(u => u.Id);

            user.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            user.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            // A concurrency token for use with the optimistic concurrency checking
            user.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            user.Property(u => u.UserName).HasMaxLength(256);
            user.Property(u => u.NormalizedUserName).HasMaxLength(256);
            user.Property(u => u.Email).HasMaxLength(256);
            user.Property(u => u.NormalizedEmail).HasMaxLength(256);
            user.Property(u => u.RefreshToken).HasMaxLength(256);
            user.HasMany<IdentityUserRole<int>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();

            var role = modelBuilder.Entity<IdentityRole<int>>()
                .ToTable("Role");

            role.HasKey(r => r.Id);
            role.Property(r => r.Name).HasMaxLength(256);
            role.Property(r => r.NormalizedName).HasMaxLength(256);

            // A concurrency token for use with the optimistic concurrency checking
            role.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Each Role can have many entries in the UserRole join table
            role.HasMany<IdentityUserRole<int>>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            var userRole = modelBuilder.Entity<IdentityUserRole<int>>()
                .ToTable("UserRole");
            userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Ignore<IdentityUserLogin<int>>();
            modelBuilder.Ignore<IdentityUserClaim<int>>();
            modelBuilder.Ignore<IdentityRoleClaim<int>>();
            modelBuilder.Ignore<IdentityUserToken<int>>();
        }
    }
}
