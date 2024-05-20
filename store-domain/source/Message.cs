namespace Store.Domain;

public record Message
{
    public Message(MessageType type, long position)
    {
        Type = type;
        Position = position;
    }

    public Message(Guid id, string type, DateTime timestamp, long position)
    {
        Id = id;
        Type = MessageType.Create(type);
        Timestamp = timestamp;
        Position = position;
    }

    public Guid Id { get; } = NewGuid();
    public long Position { get; }
    public DateTime Timestamp { get; }
    public MessageType Type { get; }
}
