
using ReagenBack.Core.Models;
using ReagenBack.Core.Repositories;

namespace ReagenBack.Core.Services;

public class VialService(
    VialRepository vialRepository,
    VialMapper vialMapper
) : CrudServiceBase<Vial, VialCreateDto, VialReadDto>(vialRepository, vialMapper)
{}