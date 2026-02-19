
using Microsoft.AspNetCore.Mvc;
using ReagenBack.Core.Contexts;
using ReagenBack.Core.Models;

namespace ReagenBack.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PackagesController(
    AppDbContext context,
    IMapper<Package, PackageCreateDto, PackageReadDto> mapper
) : CrudControllerBase<Package, PackageCreateDto, PackageReadDto>(context, mapper)
{

    // TODO: Adicionar regra de validação de tamanho 
}
