
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<NamedOption> NamedOptions { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<ControlAgency> ControlAgencies { get; set; } = null!;
    public DbSet<Brand> Brands { get; set; } = null!;
    public DbSet<Laboratory> Laboratories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Supplier>().ToTable("Suppliers");
        modelBuilder.Entity<Brand>().ToTable("Brands");
        modelBuilder.Entity<ControlAgency>().ToTable("ControlAgencies");
        modelBuilder.Entity<Laboratory>().ToTable("Laboratories");

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Reagent>()
            .HasMany<Package>()
            .WithOne(p => p.Reagent)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Package>()
            .HasMany<Vial>()
            .WithOne(v => v.Package)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Package>()
            .HasOne(p => p.Brand)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Package>()
            .HasOne(p => p.Supplier)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Reagent>()
            .HasOne(r => r.ControlAgency)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Package>()
            .OwnsOne(p => p.Size);

        modelBuilder.Entity<Reagent>()
            .OwnsMany(p => p.Sizes);
    }
}
