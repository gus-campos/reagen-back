
using Microsoft.AspNetCore.Mvc;
using ReagenBack.Core.Models;
using ReagenBack.Core.Services;

namespace ReagenBack.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SizesController(
    SizeService sizeService
) : CrudControllerBase<Size, SizeCreateDto, SizeReadDto>(sizeService)
{
    // TODO: Adicionar regras de validação de tamanho 
}
