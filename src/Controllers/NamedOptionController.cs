
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NamedOptionsControllerBase<T> : CrudControllerBase<T> where T : NamedOption, IWithId
{
    public NamedOptionsControllerBase(AppDbContext context) : base(context) { }
}

[Route("api/suppliers")]
public class SuppliersController : NamedOptionsControllerBase<Supplier>
{
    public SuppliersController(AppDbContext context) : base(context) { }
}

[Route("api/laboratories")]
public class LaboratoriesController : NamedOptionsControllerBase<Laboratory>
{
    public LaboratoriesController(AppDbContext context) : base(context) { }
}

[Route("api/control-agencies")]
public class ControlAgenciesController : NamedOptionsControllerBase<ControlAgency>
{
    public ControlAgenciesController(AppDbContext context) : base(context) { }
}

[Route("api/brands")]
public class BrandsController : NamedOptionsControllerBase<Brand>
{
    public BrandsController(AppDbContext context) : base(context) { }
}