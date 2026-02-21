
using ReagenBack.Core.Models;
using ReagenBack.Core.Repositories;

namespace ReagenBack.Core.Services;

public class ReagentService(
    ReagentRepository reagentRepository,
    ReagentMapper reagentMapper
) : CrudServiceBase<Reagent, ReagentCreateDto, ReagentReadDto>(reagentRepository, reagentMapper)
{}