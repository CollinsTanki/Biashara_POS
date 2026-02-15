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
        public DbSet<Product> Products { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }

        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Quotation> Quotations { get; set; }
        public DbSet<QuotationItem> QuotationItems { get; set; }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ImportLog> ImportLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =========================================
            // UNIQUE CONSTRAINTS
            // =========================================

            modelBuilder.Entity<PaymentMode>()
                .HasIndex(p => p.ModeName)
                .IsUnique();

            modelBuilder.Entity<StockCategory>()
                .HasIndex(c => c.CategoryName)
                .IsUnique();

            modelBuilder.Entity<StockMeasure>()
                .HasIndex(m => m.MeasureName)
                .IsUnique();

            modelBuilder.Entity<StockMeasure>()
                .HasIndex(m => m.Initials)
                .IsUnique();

            modelBuilder.Entity<StockSubCategory>()
                .HasIndex(s => new { s.StockCategoryId, s.SubCategoryName })
                .IsUnique();

            modelBuilder.Entity<UserGroup>()
                .HasIndex(g => g.GroupName)
                .IsUnique();

            modelBuilder.Entity<VatSetup>()
                .HasIndex(v => v.VatInitials)
                .IsUnique();

            modelBuilder.Entity<AppFunction>()
                .HasIndex(f => new { f.ModuleId, f.FunctionName })
                .IsUnique();

            modelBuilder.Entity<GroupPermission>()
                .HasIndex(gp => new { gp.UserGroupId, gp.AppFunctionId })
                .IsUnique();

            modelBuilder.Entity<Module>()
                .HasIndex(m => m.ModuleName)
                .IsUnique();

            modelBuilder.Entity<Branch>()
                .HasIndex(b => new { b.CompanyId, b.BranchName })
                .IsUnique();

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.CompanyName)
                .IsUnique();

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Email)
                .IsUnique();

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

            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.StockCategoryId, p.ProductName })
                .IsUnique();

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Barcode)
                .IsUnique()
                .HasFilter("[Barcode] IS NOT NULL");

            modelBuilder.Entity<Sale>()
                .HasIndex(s => s.ReceiptNumber)
                .IsUnique();

            modelBuilder.Entity<Quotation>()
                .HasIndex(q => q.RefNumber)
                .IsUnique();

            // =========================================
            // CHECK CONSTRAINTS
            // =========================================

            modelBuilder.Entity<Product>()
                .HasCheckConstraint(
                    "CK_Product_SellingPrice",
                    "[SellingPrice] >= [BuyingPrice]");

            // =========================================
            // DECIMAL PRECISION
            // =========================================

            modelBuilder.Entity<Product>()
                .Property(p => p.BuyingPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.SellingPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Sale>()
                .Property(s => s.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Sale>()
                .Property(s => s.Balance)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Customer>()
                .Property(c => c.BalanceBroughtForward)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Purchase>()
                .Property(p => p.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Quotation>()
                .Property(q => q.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<VatSetup>()
                .Property(v => v.TaxRate)
                .HasPrecision(18, 4);

            // =========================================
            // RELATIONSHIPS
            // =========================================

            // Product Relationships
            modelBuilder.Entity<Product>()
                .HasOne(p => p.StockCategory)
                .WithMany()
                .HasForeignKey(p => p.StockCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.StockSubCategory)
                .WithMany()
                .HasForeignKey(p => p.StockSubCategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.StockMeasure)
                .WithMany()
                .HasForeignKey(p => p.StockMeasureId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.VatSetup)
                .WithMany()
                .HasForeignKey(p => p.VatSetupId)
                .OnDelete(DeleteBehavior.Restrict);

            // Customer - Sale (Fix Shadow FK Warning)
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Sales)
                .WithOne(s => s.Customer)
                .HasForeignKey(s => s.CustomerId)
                .OnDelete(DeleteBehavior.SetNull);

            // Sale - User
            modelBuilder.Entity<Sale>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // SaleItem
            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Sale)
                .WithMany(s => s.SaleItems)
                .HasForeignKey(si => si.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            // Payment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Sale)
                .WithMany(s => s.Payments)
                .HasForeignKey(p => p.SaleId)
                .OnDelete(DeleteBehavior.Cascade);

            // QuotationItem
            modelBuilder.Entity<QuotationItem>()
                .HasOne(qi => qi.Quotation)
                .WithMany(q => q.QuotationItems)
                .HasForeignKey(qi => qi.QuotationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuotationItem>()
                .HasOne(qi => qi.Product)
                .WithMany()
                .HasForeignKey(qi => qi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // PurchaseItem
            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.Purchase)
                .WithMany(p => p.PurchaseItems)
                .HasForeignKey(pi => pi.PurchaseId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PurchaseItem>()
                .HasOne(pi => pi.Product)
                .WithMany()
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
