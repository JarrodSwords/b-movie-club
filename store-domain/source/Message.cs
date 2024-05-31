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

public class MessageId : TinyType<Guid>
{
    public MessageId() : this(NewGuid())
    {
    }

    public MessageId(Guid value) : base(value)
    {
    }

    public static implicit operator MessageId(Guid source) => new(source);
}
