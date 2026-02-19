
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PackagesController : CrudControllerBase<Package, PackageCreateDto, PackageReadDto>
{
    public PackagesController(
        AppDbContext context, 
        IMapper<Package, PackageCreateDto, PackageReadDto> mapper
    ) : base(context, mapper) {}

    // TODO: Adicionar regra de validação de tamanho 
}
