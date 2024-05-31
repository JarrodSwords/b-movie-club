namespace Store.Domain;

public record CandidateMessage(
    object Data,
    Position Expected
)
{
    public MessageId MessageId { get; } = new();

    //public virtual bool Equals(Message message) => MessageId == message.Id;
}
