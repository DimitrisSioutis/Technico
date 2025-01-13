using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

public class TechnicoDbContextFactory : IDesignTimeDbContextFactory<TechnicoDBContext>
{
    public TechnicoDBContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<TechnicoDBContext>();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) // or specify the path to your appsettings.json
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("TechnicoDBContext"));
        return new TechnicoDBContext(optionsBuilder.Options);
    }
}
