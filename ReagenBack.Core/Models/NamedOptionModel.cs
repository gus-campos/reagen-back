
public class NamedOption : IWithId
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}

public class NamedOptionCreateDto
{
    public string Name { get; set; } = default!;
}

public class NamedOptionReadDto : IWithId
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