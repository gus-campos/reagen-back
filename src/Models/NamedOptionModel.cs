public abstract class NamedOption : IWithId
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}

public class Supplier : NamedOption { }
public class ControlAgency : NamedOption { }
public class Brand : NamedOption { }
public class Laboratory : NamedOption { }