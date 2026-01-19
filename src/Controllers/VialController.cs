using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class VialsController : CrudControllerBase<Vial>
{
    public VialsController(AppDbContext context) : base(context) { }
}