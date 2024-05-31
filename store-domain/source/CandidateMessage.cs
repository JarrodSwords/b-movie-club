namespace Store.Domain;

public record CandidateMessage(
    object Data,
    Position Expected,
    StreamId StreamId
)
{
    public MessageId MessageId { get; } = new();
}
