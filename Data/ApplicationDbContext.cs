using Biashara_POS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Biashara_POS.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // REMOVE DbSet<AppUser>

        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<AppFunction> AppFunctions { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Branch> Branches { get; set; }

        public DbSet<StockCategory> StockCategories { get; set; }
        public DbSet<StockSubCategory> StockSubCategories { get; set; }
        public DbSet<StockMeasure> StockMeasures { get; set; }
        public DbSet<VatSetup> VatSetups { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<Item> Items { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------
            // UNIQUE CONSTRAINTS
            // -----------------------
            modelBuilder.Entity<PaymentMode>()
                .HasIndex(p => p.ModeName).IsUnique();

            modelBuilder.Entity<StockCategory>()
                .HasIndex(c => c.CategoryName).IsUnique();

            modelBuilder.Entity<StockMeasure>()
                .HasIndex(m => m.MeasureName).IsUnique();

            modelBuilder.Entity<StockMeasure>()
                .HasIndex(m => m.Initials).IsUnique();

            modelBuilder.Entity<StockSubCategory>()
                .HasIndex(s => new { s.StockCategoryId, s.SubCategoryName }).IsUnique();

            modelBuilder.Entity<UserGroup>()
                .HasIndex(g => g.GroupName).IsUnique();

            modelBuilder.Entity<VatSetup>()
                .HasIndex(v => v.VatInitials).IsUnique();

            modelBuilder.Entity<AppFunction>()
                .HasIndex(f => new { f.ModuleId, f.FunctionName }).IsUnique();

            modelBuilder.Entity<Branch>()
                .HasIndex(b => new { b.CompanyId, b.BranchName }).IsUnique();

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.CompanyName).IsUnique();

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Email).IsUnique();

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.KRAPin)
                .IsUnique()
                .HasFilter("[KRAPin] <> ''");

            modelBuilder.Entity<Company>()
                .HasCheckConstraint(
                    "CK_Company_RegistrationType",
                    "[RegistrationType] IN ('Individual', 'Business')");

            modelBuilder.Entity<Company>()
                .HasCheckConstraint(
                    "CK_Company_KRAPin_Business",
                    "([RegistrationType] = 'Individual') OR ([KRAPin] <> '')");

            modelBuilder.Entity<GroupPermission>()
                .HasIndex(gp => new { gp.UserGroupId, gp.AppFunctionId }).IsUnique();

            modelBuilder.Entity<Item>()
                .HasIndex(i => new { i.StockCategoryId, i.ItemName }).IsUnique();

            modelBuilder.Entity<Module>()
                .HasIndex(m => m.ModuleName).IsUnique();

            // -----------------------
            // RELATIONSHIPS
            // -----------------------

            modelBuilder.Entity<AppFunction>()
                .HasOne(f => f.Module)
                .WithMany(m => m.Functions)
                .HasForeignKey(f => f.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupPermission>()
                .HasOne(gp => gp.UserGroup)
                .WithMany(g => g.GroupPermissions)
                .HasForeignKey(gp => gp.UserGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GroupPermission>()
                .HasOne(gp => gp.AppFunction)
                .WithMany(f => f.GroupPermissions)
                .HasForeignKey(gp => gp.AppFunctionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.StockSubCategory)
                .WithMany()
                .HasForeignKey(i => i.StockSubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.StockMeasure)
                .WithMany()
                .HasForeignKey(i => i.StockMeasureId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Item>()
                .HasOne(i => i.VatSetup)
                .WithMany()
                .HasForeignKey(i => i.VatSetupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Item>()
                .HasCheckConstraint("CK_Item_SellingPrice", "[SellingPrice] >= [BuyingPrice]");
        }
    }
}

