using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using UI_Layer.Data;
using UI_Layer.Data.IdentityModel;
using UI_Layer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using UI_Layer;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Services
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    // Configure your identity options here.
    options.SignIn.RequireConfirmedAccount = true;
})
    .AddRoles<ApplicationRole>() // Add support for roles.
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<UserManager<ApplicationUser>, UserManager<ApplicationUser>>();
builder.Services.AddScoped<SignInManager<ApplicationUser>, SignInManager<ApplicationUser>>();
builder.Services.AddSingleton<LanguageService>();
builder.Services.AddLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("es"),
        new CultureInfo("my-MM"),
    };

    options.DefaultRequestCulture = new RequestCulture("my-MM");
    options.SupportedUICultures = supportedCultures;
});

#region redis
var redisConfiguration = builder.Configuration.GetSection("Redis").Get<RedisConfiguration>();

// Configure the Redis cache service
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConfiguration.ConnectionString;
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

#pragma warning disable ASP0014
app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name: "BackendSystem",
        areaName: "BackendSystem",
        pattern: "BackendSystem/{controller=Account}/{action=Login}/{id?}");

    endpoints.MapControllerRoute(
        name: "defaultArea",
        pattern: "{area:exists}/{controller=Account}/{action=Login}/{id?}");

    endpoints.MapControllerRoute(
        name: "defaultController",
        pattern: "{controller=Account}/{action=Login}/{id?}");

    endpoints.MapControllerRoute(
        name: "root",
        pattern: "/",
        defaults: new { area = "BackendSystem", controller = "Account", action = "Login" });

    endpoints.MapRazorPages();
});
#pragma warning restore ASP0014

app.Run();
