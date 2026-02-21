
using ReagenBack.Core.Contexts;
using ReagenBack.Core.Models;

namespace ReagenBack.Core.Repositories;

public class SizeRepository(
    AppDbContext context
) : RepositoryBase<Size>(context)
{}