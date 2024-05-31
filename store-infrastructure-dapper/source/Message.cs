namespace Store.Infrastructure.Dapper;

public record Message(
    Guid Id,
    string Type,
    uint Position,
    ulong GlobalPosition
)
{
    public DateTime Timestamp { get; } = DateTime.UtcNow;

    public static implicit operator Message(DomainMessage source) =>
        new(
            source.Id,
            source.Type,
            source.Metadata.Position,
            0
        );

    public static implicit operator DomainMessage(Message source) =>
        new(
            source.Id,
            source.Type,
            source.Timestamp,
            source.Position,
            source.GlobalPosition
        );
}
