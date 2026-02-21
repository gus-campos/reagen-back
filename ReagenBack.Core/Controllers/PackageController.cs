
using Microsoft.AspNetCore.Mvc;
using ReagenBack.Core.Models;
using ReagenBack.Core.Services;

namespace ReagenBack.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PackagesController(
    PackageService packageService
) : CrudControllerBase<Package, PackageCreateDto, PackageReadDto>(packageService)
{
    // TODO: Adicionar regra de validação de tamanho 
}
