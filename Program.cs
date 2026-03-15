using Biashara_POS.Data;
using Biashara_POS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ===============================
// DATABASE CONNECTION
// ===============================
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();


// IDENTITY CONFIGURATION
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    // Password Settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;

    // User Settings
    options.User.RequireUniqueEmail = true;

    // Sign-in settings
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


// FIX REGISTER CRASH (EMAIL SENDER)

builder.Services.AddSingleton<IEmailSender, DummyEmailSender>();


// COOKIE CONFIGURATION
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.LogoutPath = "/Identity/Account/Logout";

    options.ExpireTimeSpan = TimeSpan.FromHours(8);
});


// ===============================
// MVC + RAZOR PAGES
// ===============================
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();


// ===============================
// MIDDLEWARE PIPELINE
// ===============================
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();


// ===============================
// STATIC FILES
// ===============================
app.UseStaticFiles();

app.UseRouting();


// ===============================
// AUTHENTICATION + AUTHORIZATION
// ===============================
app.UseAuthentication();
app.UseAuthorization();


// ===============================
// ROUTING
// ===============================
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// Identity Razor Pages
app.MapRazorPages();

app.Run();


// ======================================================
// DUMMY EMAIL SENDER (PREVENTS REGISTER PAGE CRASH)
// ======================================================
public class DummyEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // No email sending required for POS
        return Task.CompletedTask;
    }
}