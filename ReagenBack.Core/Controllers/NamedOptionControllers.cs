
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ReagenBack.Core.Models;
using ReagenBack.Core.Services;

namespace ReagenBack.Core.Controllers;

public abstract class NamedOptionsControllerBase<TEntity, TCreateDto, TReadDto>(
    ICrudService<TEntity, TCreateDto, TReadDto> crudService
) : CrudControllerBase<TEntity, TCreateDto, TReadDto>(crudService)
    where TEntity : NamedOption
    where TCreateDto : class
    where TReadDto : class, IEntity
{
    // Nome repetido viola o índice único (UserId, Name)

    public override async Task<ActionResult<TReadDto>> Post(TCreateDto createDto)
    {
        try { return await base.Post(createDto); }
        catch (DbUpdateException e) when (IsUniqueViolation(e)) { return DuplicateName(); }
    }

    public override async Task<IActionResult> Put(int id, TCreateDto createDto)
    {
        try { return await base.Put(id, createDto); }
        catch (DbUpdateException e) when (IsUniqueViolation(e)) { return DuplicateName(); }
    }

    private static bool IsUniqueViolation(DbUpdateException e) =>
        e.InnerException is PostgresException { SqlState: PostgresErrorCodes.UniqueViolation };

    private ConflictObjectResult DuplicateName() =>
        Conflict(new { message = "Já existe um item com esse nome." });
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
