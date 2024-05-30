namespace Store.Infrastructure.Dapper;

public record Message(
    Guid Id,
    string Type,
    ulong Position,
    ulong GlobalPosition
)
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;

    public static implicit operator Message(DomainMessage source) => new(source.Id, source.Type, source.Position, 0);

    public static implicit operator DomainMessage(Message source) =>
        new(
            source.Id,
            source.Type,
            source.Timestamp,
            source.Position,
            source.GlobalPosition
        );
}
