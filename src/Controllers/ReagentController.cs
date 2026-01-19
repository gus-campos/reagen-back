
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReagentsController : CrudControllerBase<Reagent>
{
    public ReagentsController(AppDbContext context) : base(context) { }

    [HttpPost]
    public override async Task<ActionResult<Reagent>> Post(Reagent reagent)
    {
        // Se for passado, afirmar que já deve existir
        if (reagent.ControlAgency != null)
            _context.Attach(reagent.ControlAgency);

        return await base.Post(reagent);
    }
}