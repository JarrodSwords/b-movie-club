namespace Store.Domain;

public record CandidateMessage(
    MessageType MessageType,
    object Data,
    ulong ExpectedPosition
)
{
    public MessageId MessageId { get; } = new();

    //public virtual bool Equals(Message message) => MessageId == message.Id;
}
