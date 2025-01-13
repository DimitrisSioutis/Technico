using Microsoft.EntityFrameworkCore;
using Technico.Models;

public class TechnicoDBContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Repair> Repairs { get; set; }
    public DbSet<Property> Properties { get; set; }

    public TechnicoDBContext(DbContextOptions<TechnicoDBContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=Technico;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Repair>()
            .Property(r => r.Cost)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Property>()
            .HasOne(p => p.Owner)
            .WithMany(u => u.Properties)
            .HasForeignKey(p => p.OwnerID);

        modelBuilder.Entity<Repair>()
            .HasOne(r => r.RepairingProperty)
            .WithMany(u => u.Repairs)
            .HasForeignKey(r => r.PropertyId);
    }
}
