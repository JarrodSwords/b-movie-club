namespace Store;

public record Message
{
    public Guid Id { get; } = NewGuid();
}
