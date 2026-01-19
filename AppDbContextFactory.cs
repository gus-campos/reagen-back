using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseNpgsql("Host=db;Port=5432;Username=admin;Password=senha;Database=reagen");

        return new AppDbContext(optionsBuilder.Options);
    }
}
