
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {}

    public DbSet<Package> Packages { get; set; } = null!;
    public DbSet<Vial> Vials { get; set; } = null!;
    public DbSet<Reagent> Reagents { get; set; } = null!;
    public DbSet<Measure.Size> Sizes { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<ControlAgency> ControlAgencies { get; set; } = null!;
    public DbSet<FundingSource> FundingSources { get; set; } = null!;
    public DbSet<Laboratory> Laboratories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TABLES

        modelBuilder.Entity<Package>().ToTable("Packages");
        modelBuilder.Entity<Vial>().ToTable("Vials");
        modelBuilder.Entity<Reagent>().ToTable("Reagents");
        modelBuilder.Entity<Measure.Size>().ToTable("Sizes");
        modelBuilder.Entity<Supplier>().ToTable("Suppliers");
        modelBuilder.Entity<FundingSource>().ToTable("FundingSources");
        modelBuilder.Entity<ControlAgency>().ToTable("ControlAgencies");
        modelBuilder.Entity<Laboratory>().ToTable("Laboratories");

        base.OnModelCreating(modelBuilder);

        // RELATIOSHIPS

        // Package

        modelBuilder.Entity<Package>()
            .HasOne(p => p.Size)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Package>()
            .HasOne(p => p.Reagent)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Package>()
            .HasOne(p => p.FundingSource)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Package>()
            .HasOne(p => p.Supplier)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        // Vial

        modelBuilder.Entity<Vial>()
            .HasOne(v => v.Package)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<Vial>()
            .HasOne(v => v.Laboratory)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        // Reagent

        modelBuilder.Entity<Reagent>()
            .HasOne(r => r.ControlAgency)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        // Size

        modelBuilder.Entity<Measure.Size>()
            .HasOne(s => s.Reagent)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        // AUTO INCLUDE

        // Reagent

        modelBuilder.Entity<Reagent>()
            .Navigation(r => r.ControlAgency)
            .AutoInclude();

        // Package

        modelBuilder.Entity<Package>()
            .Navigation(p => p.Reagent)
            .AutoInclude();

        modelBuilder.Entity<Package>()
            .Navigation(p => p.FundingSource)
            .AutoInclude();

        modelBuilder.Entity<Package>()
            .Navigation(p => p.Supplier)
            .AutoInclude();

        // Vial

        modelBuilder.Entity<Vial>()
            .Navigation(v => v.Laboratory)
            .AutoInclude();
        
        modelBuilder.Entity<Vial>()
            .Navigation(v => v.Package)
            .AutoInclude();

        // Size

        modelBuilder.Entity<Measure.Size>()
            .Navigation(s => s.Reagent)
            .AutoInclude();
    }
}
