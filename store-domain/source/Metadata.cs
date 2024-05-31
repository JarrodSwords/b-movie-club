namespace Store.Domain;

public class GlobalPosition : TinyType<ulong>
{
    public GlobalPosition(ulong value) : base(value)
    {
    }

    public static implicit operator GlobalPosition(ulong source) => new(source);
}

public class Position : TinyType<uint>
{
    public Position(uint value) : base(value)
    {
    }

    public static implicit operator Position(uint source) => new(source);
}

public record Metadata(GlobalPosition GlobalPosition, Position Position, DateTime Timestamp);
