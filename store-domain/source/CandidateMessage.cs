namespace Store.Domain;

public record CandidateMessage(
    MessageType MessageType,
    object Data,
    ulong ExpectedPosition
)
{
    public MessageId MessageId { get; } = new();
}
