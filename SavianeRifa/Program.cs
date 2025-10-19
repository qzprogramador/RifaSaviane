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

// Ensure database is created/migrated and seed data at startup
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var db = services.GetRequiredService<SavianeRifa.Data.AppDbContext>();
        SavianeRifa.Data.DbInitializer.EnsureSeedData(db);
    }
    catch (Exception ex)
    {
        var logger = services.GetService<ILogger<Program>>();
        logger?.LogError(ex, "An error occurred creating or migrating the database.");
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
