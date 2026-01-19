
using Measure;

public class Package : IWithId
{
    public int Id { get; set; } = default!;
    public Size Size { get; set; } = default!;
    public double Purity { get; set; }

    // Datas
    public DateTime InDate { get; set; }
    public DateTime ExpireDate { get; set; }

    // Associações
    public Reagent Reagent { get; set; } = default!;
    public Brand? Brand { get; set; }
    public Supplier? Supplier { get; set; }
}
