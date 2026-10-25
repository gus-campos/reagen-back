
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ReagenBack.Core.Models;
using ReagenBack.Core.Utils;

namespace ReagenBack.Core.Contexts;

public class AppDbContext(
    DbContextOptions<AppDbContext> options,
    IStructuredHttpContext structuredHttpContext
) : DbContext(options)
{   
    public DbSet<Package> Packages { get; set; } = null!;
    public DbSet<Vial> Vials { get; set; } = null!;
    public DbSet<Reagent> Reagents { get; set; } = null!;
    public DbSet<Size> Sizes { get; set; } = null!;
    public DbSet<Supplier> Suppliers { get; set; } = null!;
    public DbSet<ControlAgency> ControlAgencies { get; set; } = null!;
    public DbSet<FundingSource> FundingSources { get; set; } = null!;
    public DbSet<Laboratory> Laboratories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Necessário materializar antes de iterar
        var modelEntityTypes = modelBuilder.Model.GetEntityTypes().ToList();
        foreach (var entityType in modelEntityTypes)
        {   
            // Se implementa interface
            if (!typeof(IOwnedEntity).IsAssignableFrom(entityType.ClrType))
                continue;

            var ownedEntityType = entityType;

            // Construir filter de UserId

            var parameter = Expression.Parameter(ownedEntityType.ClrType, "entity");
            var userId = Expression.Property(parameter, nameof(IOwnedEntity.UserId));
            var currentUserId = Expression.Call(
                Expression.Constant(this),
                typeof(AppDbContext).GetMethod(nameof(GetCurrentUserId), BindingFlags.NonPublic | BindingFlags.Instance)!
            );
            var convert = Expression.Convert(userId, typeof(int?));
            var filter = Expression.Lambda(Expression.Equal(convert, currentUserId), parameter);

            // Atribuir filtro
            modelBuilder.Entity(ownedEntityType.ClrType)
                .HasQueryFilter(filter);
        }

        /////////////////////////////////////////////
        base.OnModelCreating(modelBuilder);
        ////////////////////////////////////////////
        
        modelEntityTypes = [..modelBuilder.Model.GetEntityTypes()];
        foreach (var entityType in modelEntityTypes)
        {   
            // Se implementa interface
            if (!typeof(IOwnedEntity).IsAssignableFrom(entityType.ClrType))
                continue;

            var ownedEntityType = entityType;

            // Expecificar que UserId é um FK em todas entidades de IOwnedEntity
            modelBuilder.Entity(ownedEntityType.ClrType)
                .HasOne(typeof(User))
                .WithMany()
                .HasForeignKey(nameof(IOwnedEntity.UserId))
                .OnDelete(DeleteBehavior.Cascade);
        }

        // TABLES

        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Package>().ToTable("Packages");
        modelBuilder.Entity<Vial>().ToTable("Vials");
        modelBuilder.Entity<Reagent>().ToTable("Reagents");
        modelBuilder.Entity<Size>().ToTable("Sizes");
        modelBuilder.Entity<Supplier>().ToTable("Suppliers");
        modelBuilder.Entity<FundingSource>().ToTable("FundingSources");
        modelBuilder.Entity<ControlAgency>().ToTable("ControlAgencies");
        modelBuilder.Entity<Laboratory>().ToTable("Laboratories");

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

        modelBuilder.Entity<Size>()
            .HasOne(s => s.Reagent)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        // AUTO INCLUDE

        // Reagent

        modelBuilder.Entity<Reagent>()
            .Navigation(r => r.ControlAgency)
            .AutoInclude();

        // Size

        modelBuilder.Entity<Size>()
            .Navigation(s => s.Reagent)
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

        modelBuilder.Entity<Package>()
            .Navigation(p => p.Size)
            .AutoInclude();

        // Vial

        modelBuilder.Entity<Vial>()
            .Navigation(v => v.Laboratory)
            .AutoInclude();
        
        modelBuilder.Entity<Vial>()
            .Navigation(v => v.Package)
            .AutoInclude();

        // Size

        modelBuilder.Entity<Size>()
            .Navigation(s => s.Reagent)
            .AutoInclude();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        var currentUserId = GetCurrentUserId();

        var entries = ChangeTracker.Entries<IOwnedEntity>()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);
            
        foreach (var entry in entries)
        {
            // FIXME
            if (entry.Entity.UserId != currentUserId)
                throw new Exception("Não pode manipular instâncias de outros usuários.");

            foreach (var reference in entry.References)
            {
                if (reference.TargetEntry?.Entity is IOwnedEntity referenced)
                {
                    if (referenced.UserId != currentUserId)
                        throw new Exception("Não pode referenciar entidades de outros usuários.");
                }
            }
        }

        return await base.SaveChangesAsync(ct);
    }

    // Obter auth provider id a partir do contexto, capturado em middleware
    private int? GetCurrentUserId()
    {
        return structuredHttpContext.UserId;
    }
}
