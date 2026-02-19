
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SizesController : CrudControllerBase<Measure.Size, Measure.SizeCreateDto, Measure.SizeReadDto>
{
    public SizesController(
        AppDbContext context, 
        IMapper<Measure.Size, Measure.SizeCreateDto, Measure.SizeReadDto> mapper
    ) : base(context, mapper) {}

    // TODO: Adicionar regras de validação de tamanho 
}
