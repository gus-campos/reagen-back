
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PackagesController : CrudControllerBase<Package>
{
    public PackagesController(AppDbContext context) : base(context) { }

    // TODO: Adicionar regra de validação de tamanho 
}
