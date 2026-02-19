using Microsoft.AspNetCore.Mvc;
using ReagenBack.Core.Models;

namespace ReagenBack.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VialsController(
    Contexts.AppDbContext context,
    IMapper<Vial, VialCreateDto, VialReadDto> mapper
) : CrudControllerBase<Vial, VialCreateDto, VialReadDto>(context, mapper)
{}