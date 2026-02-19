
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ReagentsController : CrudControllerBase<Reagent, ReagentCreateDto,  ReagentReadDto>
{
    public ReagentsController(
        AppDbContext context, 
        IMapper<Reagent, ReagentCreateDto, ReagentReadDto> mapper
    ) : base(context, mapper) {}
    
    // FIXME: Adicionar validação para tamanho único.

    [HttpPost]
    public override async Task<ActionResult<ReagentReadDto>> Post(ReagentCreateDto reagent)
    {
        // FIXME: Corrigir isso, pensando em DTO e inclusão de FK
        // TODO: Pensar, sempre exposto por DTO? E quando for ser criado, outro DTO?

        if (reagent.ControlAgencyId != null)
        {
            var existingAgency = await _context.ControlAgencies
                .FirstOrDefaultAsync(agency => agency.Id == reagent.ControlAgencyId);

            if (existingAgency != null)
            {
                reagent.ControlAgencyId = existingAgency.Id;
            }
        }

        return await base.Post(reagent);
    }
}