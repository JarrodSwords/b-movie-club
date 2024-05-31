namespace Store.Domain;

public record Message
{
    public Message(Guid id, string type, DateTime timestamp, uint position, ulong globalPosition)
    {
        Id = id;
        Type = MessageType.Create(type);
        Metadata = new Metadata(globalPosition, position, timestamp);
    }

    public MessageId Id { get; }
    public Metadata Metadata { get; }
    public MessageType Type { get; }
}
