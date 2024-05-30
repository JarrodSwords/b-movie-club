namespace Store.Domain;

public interface IMessageStore
{
    Result<Message> Find(MessageId id);
    Result Push(CandidateMessage candidateMessage);
}
