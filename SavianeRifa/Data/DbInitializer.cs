using SavianeRifa.Models;
using Microsoft.EntityFrameworkCore;

namespace SavianeRifa.Data
{
    public static class DbInitializer
    {
        public static void EnsureSeedData(AppDbContext context)
        {
            // Ensure database/tables exist BEFORE querying.
            try
            {
                // If there are migrations in the assembly, apply them.
                var pending = context.Database.GetPendingMigrations();
                if (pending != null && pending.Any())
                {
                    context.Database.Migrate();
                }
                else
                {
                    // No migrations available: ensure DB and tables are created from the model
                    context.Database.EnsureCreated();
                }
            }
            catch
            {
                // best-effort: try EnsureCreated if Migrate failed for any reason
                try { context.Database.EnsureCreated(); } catch { }
            }

            // If there are no rifas, seed 500 available rifas
            bool hasAnyRifas = false;
            try
            {
                hasAnyRifas = context.Rifas.Any();
            }
            catch
            {
                // If table still missing, treat as empty and proceed to seed after attempting creation
                hasAnyRifas = false;
            }

            if (!hasAnyRifas)
            {
                var rifas = new List<Rifa>(capacity: 500);
                for (int i = 1; i <= 500; i++)
                {
                    rifas.Add(new Rifa
                    {
                        Number = i.ToString("D3"),
                        Status = "Disponivel",
                        Price = 10.00M
                    });
                }

                context.Rifas.AddRange(rifas);
                context.SaveChanges();
            }
        }
    }
}
