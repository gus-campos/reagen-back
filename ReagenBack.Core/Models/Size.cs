using System.Text.Json.Serialization;

namespace ReagenBack.Core.Models;

public enum Dimension
{
    Mass = 0,
    Volume = 1,
    Count = 2,
    Matter = 3
}

public enum Unit
{
    Kilogram = 0,
    Gram = 1,
    Milligram = 2,
    Liter = 3,
    Milliliter = 4,
    Mol = 5,
    Units = 6
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
    public int ReagentId { get; set; }
}

public class SizeMapper(
    // ReagentMapper reagentMapper
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

    public SizeReadDto ToReadDto(Size size)
    {
        return new()
        {
            Id = size.Id,
            Amount = size.Amount,
            Unit = size.Unit,
            ReagentId = size.ReagentId
        };
    }
}