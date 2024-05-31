namespace Store.Domain;

public class Position : TinyType<uint>
{
    public Position(uint value) : base(value)
    {
    }

    public static implicit operator Position(uint source) => new(source);
}
