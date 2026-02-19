using Microsoft.AspNetCore.Mvc;
using ReagenBack.Core.Models;

namespace ReagenBack.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VialsController : CrudControllerBase<Vial, VialCreateDto, VialReadDto>
{
    public VialsController(
        Contexts.AppDbContext context, 
        IMapper<Vial, VialCreateDto, VialReadDto> mapper
    ) : base(context, mapper) {}
}