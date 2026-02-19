using System.Text.Json.Serialization;

namespace ReagenBack.Core.Models;

public enum Dimension
{
    Mass,
    Volume,
    Count,
    Matter
}

public enum Unit
{
    Kilogram,
    Gram,
    Milligram,
    Liter,
    Milliliter,
    Mol,
    Units
}

public static class UnitDimension
{
    public static Dimension Map(Unit unit) => _unitDimension[unit];

    private static readonly Dictionary<Unit, Dimension> _unitDimension = new()
    {
        { Unit.Kilogram, Dimension.Mass },
        { Unit.Gram, Dimension.Mass },
        { Unit.Milligram, Dimension.Mass },
        { Unit.Liter, Dimension.Volume },
        { Unit.Milliliter, Dimension.Volume },
        { Unit.Mol, Dimension.Matter },
        { Unit.Units, Dimension.Count },
    };
}

public class Size : IWithId
{
    public int Id { get; set;}
    public double Amount { get; set; }
    public Unit Unit { get; set; }

    public int ReagentId { get; set; }
    public Reagent Reagent { get; set; } = default!;

    [JsonIgnore]
    public Dimension Dimension => UnitDimension.Map(Unit);
}

public class SizeCreateDto
{
    public double Amount { get; set; }
    public Unit Unit { get; set; }
    public int ReagentId { get; set; }
}

public class SizeReadDto : IWithId
{
    public int Id { get; set;}
    public double Amount { get; set; }
    public Unit Unit { get; set; }
    public ReagentReadDto Reagent { get; set; } = default!;
}

public class SizeMapper(
    ReagentMapper reagentMapper
) : IMapper<Size, SizeCreateDto, SizeReadDto>
{
    public Size ToEntity(SizeCreateDto dto, int id)
    {    
        return new()
        {    
            Id = id,
            Amount = dto.Amount,
            Unit = dto.Unit,
            ReagentId = dto.ReagentId,
        };
    } 

    public SizeReadDto ToReadDto(Size vial)
    {
        return new()
        {
            Id = vial.Id,
            Amount = vial.Amount,
            Unit = vial.Unit,
            Reagent = reagentMapper.ToReadDto(vial.Reagent),
        };
    }
}