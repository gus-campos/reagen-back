
using Measure;

public class Reagent : IWithId
{
    public int Id { get; set; } = default!;
    public string Name { get; set; } = default!;
    public Dimension Dimension { get; set; } = default!;
    public List<Size> Sizes { get; set; } = new List<Size>();
    public ControlAgency? ControlAgency { get; set; }
}