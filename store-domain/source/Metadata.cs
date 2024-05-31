namespace Store.Domain;

public class Metadata : ValueObject
{
    public Metadata(GlobalPosition globalPosition, Position position, DateTime timestamp)
    {
        GlobalPosition = globalPosition;
        Position = position;
        Timestamp = timestamp;
    }

    public GlobalPosition GlobalPosition { get; }
    public Position Position { get; }
    public DateTime Timestamp { get; }

    #region Equality

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return GlobalPosition;
        yield return Position;
        yield return Timestamp;
    }

    #endregion
}
