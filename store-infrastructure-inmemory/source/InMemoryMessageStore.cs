using Stream = Store.Domain.Stream;

namespace Store.Infrastructure.InMemory;

public class InMemoryMessageStore : IMessageStore
{
    private readonly List<Message> _messages = new();

    private readonly ISerializer _serializer = new DefaultSerializer();

    public Result<Message> Find(MessageId id) => _messages.Single(x => x.Id == id);
    public Result<Stream> Find(StreamId id) => throw new NotImplementedException();

    public Result Push(CandidateMessage candidateMessage)
    {
        var position = _messages.Any(x => x.Metadata.StreamId == candidateMessage.StreamId)
            ? _messages
                  .Where(x => x.Metadata.StreamId == candidateMessage.StreamId)
                  .Max(x => (uint) x.Metadata.Position)!
            + 1
            : 0;

        var message = new Message(
            candidateMessage.MessageId,
            candidateMessage.StreamId.EntityId,
            candidateMessage.StreamId.Category,
            _serializer.Serialize(candidateMessage.Data),
            (ulong) _messages.Count,
            candidateMessage.StreamId.IsCommand,
            position,
            UtcNow,
            candidateMessage.Data.GetType().Name
        );

        _messages.Add(message);

        return Success();
    }
}
