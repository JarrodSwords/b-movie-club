namespace Store.Domain;

public class GlobalPosition : TinyType<ulong>
{
    public GlobalPosition(ulong value) : base(value)
    {
    }

    public static implicit operator GlobalPosition(ulong source) => new(source);
}
