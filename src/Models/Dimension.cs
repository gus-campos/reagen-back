using System.Text.Json.Serialization;

namespace Measure;

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

    private static readonly IReadOnlyDictionary<Unit, Dimension> _unitDimension = new Dictionary<Unit, Dimension>
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

public record Size
{
    public double Amount { get; set; }
    public Unit Unit { get; set; }

    [JsonIgnore]
    public Dimension Dimension => Measure.UnitDimension.Map(Unit);
}
