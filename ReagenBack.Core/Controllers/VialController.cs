using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class VialsController : CrudControllerBase<Vial, VialCreateDto, VialReadDto>
{
    public VialsController(
        AppDbContext context, 
        IMapper<Vial, VialCreateDto, VialReadDto> mapper
    ) : base(context, mapper) {}
}