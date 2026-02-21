
using ReagenBack.Core.Models;
using ReagenBack.Core.Repositories;

namespace ReagenBack.Core.Services;

public class PackageService(
    PackageRepository packageRepository,
    PackageMapper packageMapper
) : CrudServiceBase<Package, PackageCreateDto, PackageReadDto>(packageRepository, packageMapper)
{}