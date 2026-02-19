
using Microsoft.AspNetCore.Mvc;
using ReagenBack.Core.Models;

namespace ReagenBack.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SizesController : CrudControllerBase<Size, SizeCreateDto, SizeReadDto>
{
    public SizesController(
        Contexts.AppDbContext context, 
        IMapper<Size, SizeCreateDto, SizeReadDto> mapper
    ) : base(context, mapper) {}

    // TODO: Adicionar regras de validação de tamanho 
}
