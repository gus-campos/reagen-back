
using ReagenBack.Core.Contexts;
using ReagenBack.Core.Models;

namespace ReagenBack.Core.Repositories;

public class ReagentRepository(
    AppDbContext context
) : RepositoryBase<Reagent>(context)
{}