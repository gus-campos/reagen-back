
using Microsoft.AspNetCore.Mvc;
using ReagenBack.Core.Models;
using ReagenBack.Core.Services;

namespace ReagenBack.Core.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReagentsController(
    ReagentService reagentService
) : CrudControllerBase<Reagent, ReagentCreateDto, ReagentReadDto>(reagentService)
{
    // FIXME: Adicionar validação para tamanho único.
}