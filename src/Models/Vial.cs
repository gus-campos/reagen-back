
public class Vial : IWithId
{
    public int Id { get; set; } = default!;

    // Datas
    public DateTime? OutDate { get; set; }

    // Associações
    public Package Package { get; set; } = default!;
    public Laboratory Laboratory { get; set; } = default!;
}

// Nota para relações:
// O EF sempre vai considerar objetos passados como novas entidades, a não ser que você informe o contrário.
