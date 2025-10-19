using Microsoft.EntityFrameworkCore;
using SavianeRifa.Models;

namespace SavianeRifa.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<PaymentInformation> PaymentInformations { get; set; } = null!;
        public DbSet<Rifa> Rifas { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure 1:N relationship between PaymentInformation and Rifa
            modelBuilder.Entity<Rifa>()
                .HasOne(r => r.PaymentInformation)
                .WithMany(p => p.Rifas)
                .HasForeignKey(r => r.PaymentInformationId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
