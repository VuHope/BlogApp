using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogAppMVC.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Seed Roles (SuperAdmin, Admin, User)
            var superAdminRoleId = "2e4e839f-219e-480e-8316-462b5d3db715";
            var adminRoleId = "3d66fe3c-e49f-42e6-b6f8-9946f623aeaa";
            var userRoleId = "fa403ff4-10f9-443e-87ff-7bad1578d7b8";
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "SuperAdmin", NormalizedName = "SuperAdmin", Id = superAdminRoleId, ConcurrencyStamp = superAdminRoleId },
                new IdentityRole { Name = "Admin", NormalizedName = "Admin", Id = adminRoleId, ConcurrencyStamp = adminRoleId },
                new IdentityRole { Name = "User", NormalizedName = "User", Id = userRoleId, ConcurrencyStamp = userRoleId }
            );
            //Seed User
            var superAdminId = "472ba632-6133-44a1-b158-6c10bd7d850d";
            var superAdminUser = new IdentityUser
            {
                UserName = "superadmin@bloggie.com",
                Email = "superadmin@bloggie.com",
                NormalizedEmail = "superadmin@bloggie.com".ToUpper(),
                NormalizedUserName = "superadmin@bloggie.com".ToUpper(),
                Id = superAdminId
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
                .HashPassword(superAdminUser, "Superadmin@123");


            modelBuilder.Entity<IdentityUser>().HasData(superAdminUser);
            //Seed User Role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { RoleId = superAdminRoleId, UserId = superAdminId },
                new IdentityUserRole<string> { RoleId = adminRoleId, UserId = superAdminId },
                new IdentityUserRole<string> { RoleId = userRoleId, UserId = superAdminId }
            );
        }
    }
}
