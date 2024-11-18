using Microsoft.EntityFrameworkCore;
using Technico.Models;
namespace Technico.Context;

public class TechnicoDBContext : DbContext
{
    public DbSet<User> Users{ get; set; }
    public DbSet<Repair> Repairs{ get; set; }
    public DbSet<Property> Properties{ get; set; }

    public TechnicoDBContext(DbContextOptions<TechnicoDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Repair>()
        .Property(r => r.Cost)
        .HasPrecision(18, 2);

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server = localhost\\SQLEXPRESS; Database = Technico; Trusted_Connection = True; TrustServerCertificate = True;");
        optionsBuilder.EnableSensitiveDataLogging();
    }

}

