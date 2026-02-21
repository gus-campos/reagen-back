using ReagenBack.Core.Contexts;
using ReagenBack.Core.Models;

namespace ReagenBack.Core.Repositories;

public abstract class NamedOptionRepositoryBase<TEntity>(
    AppDbContext context
) : RepositoryBase<TEntity>(context)
where TEntity : NamedOption
{}

// Derivados
public class SupplierRepository(
    AppDbContext context
) : NamedOptionRepositoryBase<Supplier>(context)
{}

public class LaboratoryRepository(
    AppDbContext context
) : NamedOptionRepositoryBase<Laboratory>(context)
{}

public class ControlAgencyRepository(
    AppDbContext context
) : NamedOptionRepositoryBase<ControlAgency>(context)
{}

public class FundingSourceRepository(
    AppDbContext context
) : NamedOptionRepositoryBase<FundingSource>(context)
{}