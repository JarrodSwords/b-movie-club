namespace Store;

public record Message
{
    public Message(MessageType type)
    {
        Type = type;
    }

    public Guid Id { get; } = NewGuid();
    public MessageType Type { get; }
}
