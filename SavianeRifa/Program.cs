using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure EF Core with SQLite. Ensure the package Microsoft.EntityFrameworkCore.Sqlite is installed.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=rifas.db";
builder.Services.AddDbContext<SavianeRifa.Data.AppDbContext>(options =>
    options.UseSqlite(connectionString)
);

var app = builder.Build();

// Ensure database is created and migrations are applied at startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<SavianeRifa.Data.AppDbContext>();
        // Try to apply migrations; if none exist, EnsureCreated will create the DB
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetService<ILogger<Program>>();
        logger?.LogError(ex, "An error occurred creating or migrating the database.");
        // As a fallback, try EnsureCreated (useful for simple scenarios)
        try
        {
            var db = services.GetRequiredService<SavianeRifa.Data.AppDbContext>();
            db.Database.EnsureCreated();
        }
        catch { /* swallow */ }
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
