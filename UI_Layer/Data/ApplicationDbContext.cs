using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UI_Layer.Data.AppData;
using UI_Layer.Data.IdentityModel;
using UI_Layer.Data.SeedData;

namespace UI_Layer.Data
{
    public class ApplicationDbContext :  IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
     
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.CreatedUser)
                .WithMany()
                .HasForeignKey(u => u.CreatedUserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
            modelBuilder.Entity<ApplicationRole>().ToTable("ApplicationRole");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("ApplicationUserRole");
            modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserToken");
            modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaim");

            modelBuilder.Entity<CompanyInformationDM>().ToTable("CompanyInformation");
            modelBuilder.Entity<CustomerTypeDM>().ToTable("CustomerType");
            //modelBuilder.Entity<CustomerDM>().ToTable("Customer");
            #region SeedData
            ModelBuilderExtensions.SeedUsers(modelBuilder);
            ModelBuilderExtensions.SeedRoles(modelBuilder);
            ModelBuilderExtensions.SeedUserRoles(modelBuilder);
            #endregion
        }
        public DbSet<CompanyInformationDM> CompanyInformation { get; set; }
        public DbSet<CustomerTypeDM> CustomerType { get; set; }
        public DbSet<CustomerDM> Customer { get; set; }

        public DbSet<CategoryDM> Category { get; set; }
        public DbSet<ServiceStockDM> ServiceStock { get; set; }
        public DbSet<DoctorDM> Doctor { get; set; }
        public DbSet<StaffDM> Staff { get; set; }
    }
}