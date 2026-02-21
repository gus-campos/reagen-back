
using ReagenBack.Core.Contexts;
using ReagenBack.Core.Models;

namespace ReagenBack.Core.Repositories;

public class PackageRepository(
    AppDbContext context
) : RepositoryBase<Package>(context)
{}