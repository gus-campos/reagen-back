
using ReagenBack.Core.Models;
using ReagenBack.Core.Repositories;

namespace ReagenBack.Core.Services;

public class SizeService(
    SizeRepository sizeRepository,
    SizeMapper sizeMapper
) : CrudServiceBase<Size, SizeCreateDto, SizeReadDto>(sizeRepository, sizeMapper)
{}