
using ReagenBack.Core.Models;
using ReagenBack.Core.Repositories;

namespace ReagenBack.Core.Services;

public abstract class NamedOptionServiceBase<TEntity, TCreateDto, TReadDto>(
    IRepository<TEntity> repository,
    IMapper<TEntity, TCreateDto, TReadDto> mapper
) : CrudServiceBase<TEntity, TCreateDto, TReadDto>(repository, mapper)
    where TEntity : NamedOption
    where TCreateDto : class
    where TReadDto : class, IWithId
{   
    // Regras para todos
    // TODO: Impedir criação com nome repetido
}

// Derivados
public class SupplierService(
    SupplierRepository repository,
    SupplierMapper mapper
) : NamedOptionServiceBase<Supplier, SupplierCreateDto, SupplierReadDto>(repository, mapper)
{}

public class LaboratoryService(
    LaboratoryRepository repository,
    LaboratoryMapper mapper
) : NamedOptionServiceBase<Laboratory, LaboratoryCreateDto, LaboratoryReadDto>(repository, mapper)
{}

public class ControlAgencyService(
    ControlAgencyRepository repository,
    ControlAgencyMapper mapper
) : NamedOptionServiceBase<ControlAgency, ControlAgencyCreateDto, ControlAgencyReadDto>(repository, mapper)
{}

public class FundingSourceService(
    FundingSourceRepository repository,
    FundingSourceMapper mapper
) : NamedOptionServiceBase<FundingSource, FundingSourceCreateDto, FundingSourceReadDto>(repository, mapper)
{}