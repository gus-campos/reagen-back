
using Microsoft.AspNetCore.Mvc;
using ReagenBack.Core.Models;
using ReagenBack.Core.Services;

namespace ReagenBack.Core.Controllers;

public abstract class NamedOptionsControllerBase<TEntity, TCreateDto, TReadDto>(
    ICrudService<TEntity, TCreateDto, TReadDto> crudService
) : CrudControllerBase<TEntity, TCreateDto, TReadDto>(crudService)
    where TEntity : NamedOption
    where TCreateDto : class
    where TReadDto : class, IWithId
{
    // Regras para todos os controllers de named options
    // Não necessário casa não haja regras para todos
}

// Derivados

[Route("api/suppliers")]
public class SuppliersController(
    SupplierService supplierService
) : NamedOptionsControllerBase<Supplier, SupplierCreateDto, SupplierReadDto>(supplierService)
{}

[Route("api/laboratories")]
public class LaboratoriesController(
    LaboratoryService laboratoryService
) : NamedOptionsControllerBase<Laboratory, LaboratoryCreateDto, LaboratoryReadDto>(laboratoryService)
{}

[Route("api/control-agencies")]
public class ControlAgenciesController(
    ControlAgencyService controlAgencyService
) : NamedOptionsControllerBase<ControlAgency, ControlAgencyCreateDto, ControlAgencyReadDto>(controlAgencyService)
{}

[Route("api/funding-sources")]
public class FundingSourcesController(
    FundingSourceService fundingSourceService
) : NamedOptionsControllerBase<FundingSource, FundingSourceCreateDto, FundingSourceReadDto>(fundingSourceService)
{}
