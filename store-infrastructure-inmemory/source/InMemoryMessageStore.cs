namespace Store.Infrastructure.InMemory;

public class InMemoryMessageStore : IMessageStore
{
    private readonly List<Message> _messages = new();

    public Result<Message> Find(MessageId id)
    {
        var message = _messages.Single(x => x.Id == id);

        return new Message(
            message.Id,
            message.Type,
            message.Metadata.Timestamp,
            message.Metadata.Position,
            message.Metadata.GlobalPosition
        );
    }

    public Result Push(CandidateMessage candidateMessage)
    {
        var message = new Message(
            candidateMessage.MessageId,
            candidateMessage.Data.GetType().Name,
            DateTime.UtcNow,
            0,
            (ulong) _messages.Count
        );

        _messages.Add(message);

        return Success();
    }
}
