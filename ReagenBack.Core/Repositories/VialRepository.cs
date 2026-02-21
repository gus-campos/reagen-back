
using ReagenBack.Core.Contexts;
using ReagenBack.Core.Models;

namespace ReagenBack.Core.Repositories;

public class VialRepository(
    AppDbContext context
) : RepositoryBase<Vial>(context)
{}