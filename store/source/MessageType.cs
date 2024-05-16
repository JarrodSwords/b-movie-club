namespace Store;

public class MessageType : TinyType<string>
{
    private MessageType(string value) : base(value)
    {
    }

    public static Result<MessageType> Create(string value) =>
        string.IsNullOrWhiteSpace(value)
            ? NameRequired()
            : new MessageType(value);

    public static Error NameRequired() => new($"{nameof(MessageType)} name required.");
}
