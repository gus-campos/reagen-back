
using Microsoft.AspNetCore.Mvc;
using ReagenBack.Core.Models;

namespace ReagenBack.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SizesController(
    Contexts.AppDbContext context,
    IMapper<Size, SizeCreateDto, SizeReadDto> mapper
) : CrudControllerBase<Size, SizeCreateDto, SizeReadDto>(context, mapper)
{

    // TODO: Adicionar regras de validação de tamanho 
}
