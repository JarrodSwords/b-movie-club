namespace Store.Domain;

public record Message
{
    public Message(MessageType type)
    {
        Type = type;
    }

    public Message(Guid id, string type, DateTime timestamp)
    {
        Id = id;
        Type = MessageType.Create(type);
        Timestamp = timestamp;
    }

    public Guid Id { get; } = NewGuid();
    public DateTime Timestamp { get; }
    public MessageType Type { get; }
}
