
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReagentsController : CrudControllerBase<Reagent>
{
    public ReagentsController(AppDbContext context) : base(context) { }
}