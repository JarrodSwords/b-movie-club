namespace Store.Domain;

public record CandidateMessage(MessageType MessageType, ulong ExpectedPosition)
{
    public MessageId MessageId { get; } = new();
}
