namespace Store.Domain;

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
