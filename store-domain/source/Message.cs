namespace Store.Domain;

public record Message
{
    public Message(MessageType type, ulong position)
    {
        Id = new();
        Type = type;
        Position = position;
    }

    public Message(Guid id, string type, DateTime timestamp, ulong position, ulong globalPosition)
    {
        Id = id;
        Type = MessageType.Create(type);
        Timestamp = timestamp;
        Position = position;
        GlobalPosition = globalPosition;
    }

    public MessageId Id { get; }
    public ulong GlobalPosition { get; }
    public ulong Position { get; }
    public DateTime Timestamp { get; }
    public MessageType Type { get; }
}
