
using Microsoft.AspNetCore.Mvc;
using ReagenBack.Core.Models;
using ReagenBack.Core.Services;

namespace ReagenBack.Core.Controllers;

[ApiController]
[Route("api/reagents")]
public class ReagentsController(
    ReagentService reagentService
) : CrudControllerBase<Reagent, ReagentCreateDto, ReagentReadDto>(reagentService)
{
    // TODO: Adicionar validação para tamanho único.
}