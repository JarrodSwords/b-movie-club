namespace Store.Domain;

public interface IMessageStore
{
    Result<Message> Find(MessageId id);
    Result<Stream> Find(StreamId id);
    Result Push(CandidateMessage candidateMessage);
}
