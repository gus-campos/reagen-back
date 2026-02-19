
using Measure;

public class Package : IWithId
{
    public int Id { get; set; }
    public double Purity { get; set; }
    public DateTime InDate { get; set; }
    public DateTime ExpireDate { get; set; }

    public int SizeId { get; set; }
    public Size Size { get; set; } = default!;

    public int ReagentId { get; set; }
    public Reagent Reagent { get; set; } = default!;
    
    public int? FundingSourceId { get; set; }
    public FundingSource? FundingSource { get; set; }

    public int? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }
}

public class PackageCreateDto
{
    public double Purity { get; set; }
    public DateTime InDate { get; set; }
    public DateTime ExpireDate { get; set; }

    public int SizeId { get; set; }
    public int ReagentId { get; set; }
    public int? FundingSourceId { get; set; }
    public int? SupplierId { get; set; }
}

public class PackageReadDto : IWithId
{
    public int Id { get; set; }
    public double Purity { get; set; }
    public DateTime InDate { get; set; }
    public DateTime ExpireDate { get; set; }
    
    public Size Size { get; set; } = default!;
    public ReagentReadDto Reagent { get; set; } = default!;
    public FundingSourceReadDto? FundingSource { get; set; }
    public SupplierReadDto? Supplier { get; set; }
}

public class PackageMapper(
        ReagentMapper reagentMapper,
        FundingSourceMapper fundingSourceMapper,
        SupplierMapper supplierMapper
) : IMapper<Package, PackageCreateDto, PackageReadDto>
{
    public Package ToEntity(PackageCreateDto dto, int id)
    {    
        return new()
        {    
            Id = id,
            Purity = dto.Purity,
            InDate = dto.InDate,
            ExpireDate = dto.ExpireDate,

            SizeId = dto.SizeId,
            ReagentId = dto.ReagentId,
            FundingSourceId = dto.FundingSourceId,
            SupplierId = dto.SupplierId,
        };
    } 

    public PackageReadDto ToReadDto(Package package)
    {
        return new() {    
            Purity = package.Purity,
            InDate = package.InDate,
            ExpireDate = package.ExpireDate,

            Size = package.Size,
            Reagent = reagentMapper.ToReadDto(package.Reagent),
            FundingSource = package.FundingSource == null ? null : fundingSourceMapper.ToReadDto(package.FundingSource),
            Supplier = package.Supplier == null ? null : supplierMapper.ToReadDto(package.Supplier),
        };
    }
}