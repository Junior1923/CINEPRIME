using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CINE_PRIME.Data
{
    public class ContextFactory : IDesignTimeDbContextFactory<CinePrimeContext>
    {
        public CinePrimeContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CinePrimeContext>();

            // Usar configuración desde appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new CinePrimeContext(optionsBuilder.Options);
        }
    }

}
