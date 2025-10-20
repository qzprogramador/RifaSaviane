using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Server.Kestrel.Https;

var builder = WebApplication.CreateBuilder(args);

// Configurar Kestrel para usar HTTPS com certificado .pfx
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // HTTP
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps("C:\\RifaSolidaria\\saviane_ddns_net.pfx", "1234");
    });
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure CORS - allow all origins (use carefully in production)
var corsPolicyName = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName,
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

// Configure EF Core with SQL Server
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Server=DESKTOP-8I4A0RA\\ANDREY;Database=SavianeRifa;User Id=sa;Password=yerdna15043733;TrustServerCertificate=True;";
builder.Services.AddDbContext<SavianeRifa.Data.AppDbContext>(options =>
    options.UseSqlServer(connectionString)
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

// Enable CORS globally
app.UseCors(corsPolicyName);

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
