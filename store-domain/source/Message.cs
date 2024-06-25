namespace Store.Domain;

public record Message
{
    public Message(
        Guid id,
        Guid entityId,
        string category,
        string jsonData,
        ulong globalPosition,
        bool isCommand,
        uint position,
        DateTime timestamp,
        string type
    )
    {
        Id = id;
        JsonData = jsonData;
        Type = MessageType.Create(type);
        Metadata = new Metadata(
            globalPosition,
            position,
            new StreamId(Category.Create(category), new(entityId), isCommand),
            timestamp
        );
    }

    public MessageId Id { get; }
    public string JsonData { get; }
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

public interface IPayload
{
}
