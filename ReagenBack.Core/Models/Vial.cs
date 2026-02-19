
public class Vial : IWithId
{
    public int Id { get; set; }
    public DateTime? OutDate { get; set; }

    public int PackageId { get; set; }
    public Package Package { get; set; } = default!;
    
    public int LaboratoryId { get; set; }
    public Laboratory Laboratory { get; set; } = default!;
}

public class VialCreateDto
{
    public DateTime? OutDate { get; set; }
    public int PackageId { get; set; }
    public int LaboratoryId { get; set; }
}

public class VialReadDto : IWithId
{
    public int Id { get; set; }
    public DateTime? OutDate { get; set; }
    public PackageReadDto Package { get; set; } = default!;
    public LaboratoryReadDto Laboratory { get; set; } = default!;
}

public class VialMapper(
    PackageMapper packageMapper,
    LaboratoryMapper laboratoryMapper
) : IMapper<Vial, VialCreateDto, VialReadDto>
{
    public Vial ToEntity(VialCreateDto dto, int id)
    {    
        return new()
        {    
            Id = id,
            OutDate = dto.OutDate,
            PackageId = dto.PackageId,
            LaboratoryId = dto.LaboratoryId
        };
    } 

    public VialReadDto ToReadDto(Vial vial)
    {
        return new() {    
            OutDate = vial.OutDate,
            Package = packageMapper.ToReadDto(vial.Package),
            Laboratory = laboratoryMapper.ToReadDto(vial.Laboratory)
        };
    }
}