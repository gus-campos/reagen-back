
using Microsoft.EntityFrameworkCore;

namespace ReagenBack.Core.Models;

// Nome não pode se repetir para o mesmo usuário (herdado pelas derivadas)
[Index(nameof(UserId), nameof(Name), IsUnique = true)]
public class NamedOption : IOwnedEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    
    public string Name { get; set; } = default!;
}

public class NamedOptionCreateDto
{
    public string Name { get; set; } = default!;
}

public class NamedOptionReadDto : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}

public class NamedOptionMapper<TEntity, TCreateDto, TReadDto> : IMapper<TEntity, TCreateDto, TReadDto>
    where TEntity : NamedOption, new()
    where TCreateDto : NamedOptionCreateDto, new()
    where TReadDto : NamedOptionReadDto, new()
{
    public TEntity ToEntity(TCreateDto dto, int id)
    {
        return new()
        {
            Id = id,
            Name = dto.Name
        };
    }

    public TReadDto ToReadDto(TEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name
    };
}

// Derivados

public class Supplier      : NamedOption {}
public class FundingSource : NamedOption {}
public class Laboratory    : NamedOption {}
public class ControlAgency : NamedOption {}

public class SupplierCreateDto      : NamedOptionCreateDto {}
public class FundingSourceCreateDto : NamedOptionCreateDto {}
public class LaboratoryCreateDto    : NamedOptionCreateDto {}
public class ControlAgencyCreateDto : NamedOptionCreateDto {}

public class SupplierReadDto      : NamedOptionReadDto {}
public class FundingSourceReadDto : NamedOptionReadDto {}
public class LaboratoryReadDto    : NamedOptionReadDto {}
public class ControlAgencyReadDto : NamedOptionReadDto {}

public class SupplierMapper      : NamedOptionMapper<Supplier, SupplierCreateDto, SupplierReadDto> {}
public class FundingSourceMapper : NamedOptionMapper<FundingSource, FundingSourceCreateDto, FundingSourceReadDto> {}
public class LaboratoryMapper    : NamedOptionMapper<Laboratory, LaboratoryCreateDto, LaboratoryReadDto> {}
public class ControlAgencyMapper : NamedOptionMapper<ControlAgency, ControlAgencyCreateDto, ControlAgencyReadDto> {}