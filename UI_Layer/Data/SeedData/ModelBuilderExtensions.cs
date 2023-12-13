using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UI_Layer.Data.IdentityModel;
using UI_Layer.Helpers;

namespace UI_Layer.Data.SeedData
{
    public class ModelBuilderExtensions
    {
        public static Guid UserID = Guid.Parse("8d7fae5e-3cd5-497b-b334-86790609eedc");
        public static Guid AdminRole = Guid.Parse("d9cbb087-b31f-4441-8506-c21c7e0c15a4"); //Guid.Parse("");
        public static Guid CustomerRole = Guid.Parse("3b79e7b8-9a4a-41be-883a-5853056716e9");
        public static Guid StaffRole = Guid.Parse("3b79e7b8-9a4a-41be-883a-5853056716d7");
        public static void SeedUsers(ModelBuilder builder)
        {
            ApplicationUser user = new ApplicationUser()
            {

                Id = UserID,
                FullName = "Admin",
                UserName = "admin",
                Email = "admin@gmail.com",
                LockoutEnabled = false,
                PhoneNumber = "1234567890",
                EmailConfirmed = true,
                NormalizedEmail = "admin@gmail.com",
                NormalizedUserName = "ADMIN",
                #region Custom

                SystemUser = true,
                UserType = Constant.UserType.Staffs,
                #endregion

                #region Default
                CreatedUserId = UserID,
                CreatedDate = DateTime.Now,
                UpdatedUserId = UserID,
                Active = true,
                #endregion
            };

            PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, "123@Ace");
            user.SecurityStamp = Guid.NewGuid().ToString();
            builder.Entity<ApplicationUser>().HasData(user);
        }
        public static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<ApplicationRole>().HasData(
                new ApplicationRole() { Id = AdminRole, Name = "Administrator", NormalizedName = "ADMINISTRATOR", Description = "Admin Role" },
                new ApplicationRole() { Id = StaffRole, Name = "Staff", NormalizedName = "STAFF" , Description = "Staff Role" },
                new ApplicationRole() { Id = CustomerRole, Name = "Customer", NormalizedName = "Customer", Description = "Customer Role" }
            );
        }
        public static void SeedUserRoles(ModelBuilder builder)
        {
            builder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole() { RoleId = AdminRole, UserId = UserID }
            );
        }
    }
}
