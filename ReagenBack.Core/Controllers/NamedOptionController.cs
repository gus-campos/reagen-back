
using Microsoft.AspNetCore.Mvc;
using ReagenBack.Core.Contexts;
using ReagenBack.Core.Models;

namespace ReagenBack.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NamedOptionsControllerBase<TEntity, TCreateDto, TReadDto>(
    AppDbContext context,
    IMapper<TEntity, TCreateDto, TReadDto> mapper
) : CrudControllerBase<TEntity, TCreateDto, TReadDto>(context, mapper) 
    where TEntity : NamedOption
    where TReadDto : IWithId
{
    //
}

// Derivados

[Route("api/suppliers")]
public class SuppliersController 
    : NamedOptionsControllerBase<Supplier, SupplierCreateDto, SupplierReadDto>
{
    public SuppliersController(
        AppDbContext context, 
        IMapper<Supplier, SupplierCreateDto, SupplierReadDto> mapper
    ) : base(context, mapper) {}
}

[Route("api/laboratories")]
public class LaboratoriesController 
    : NamedOptionsControllerBase<Laboratory, LaboratoryCreateDto, LaboratoryReadDto>
{
    public LaboratoriesController(
        AppDbContext context, 
        IMapper<Laboratory, LaboratoryCreateDto, LaboratoryReadDto> mapper
    ) : base(context, mapper) {}
}

[Route("api/control-agencies")]
public class ControlAgenciesController 
    : NamedOptionsControllerBase<ControlAgency, ControlAgencyCreateDto, ControlAgencyReadDto>
{
    public ControlAgenciesController(
        AppDbContext context, 
        IMapper<ControlAgency, ControlAgencyCreateDto, ControlAgencyReadDto> mapper
    ) : base(context, mapper) {}
}

[Route("api/funding-sources")]
public class FundingSourcesController 
    : NamedOptionsControllerBase<FundingSource, FundingSourceCreateDto, FundingSourceReadDto>
{
    public FundingSourcesController(
        AppDbContext context, 
        IMapper<FundingSource, FundingSourceCreateDto, FundingSourceReadDto> mapper
    ) : base(context, mapper) {}
}