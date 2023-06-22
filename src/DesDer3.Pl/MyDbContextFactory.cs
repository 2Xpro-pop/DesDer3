using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using DesDer3.Dal.EfIdentity;

namespace DesDer3.Pl;

public class MyDbContextFactory : IDesignTimeDbContextFactory<DesDer3DbContext>
{
    public DesDer3DbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Environment.CurrentDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        var optionsBuilder = new DbContextOptionsBuilder<DesDer3DbContext>();
        optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("DesDer3.Pl"));

        return new DesDer3DbContext(optionsBuilder.Options);
    }
}
