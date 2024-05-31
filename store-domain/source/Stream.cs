namespace Store.Domain;

public class Stream
{
    private readonly IEnumerable<Message> _messages;

    public Stream(Category category, bool isCommand = false)
    {
        Id = new StreamId(category, new(), isCommand);
        _messages = new List<Message>();
    }

    public Stream(params Message[] messages)
    {
        _messages = messages;
    }

    public StreamId Id { get; }

    public Result<CandidateMessage> Next(
        MessageType messageType,
        object? data = null
    ) =>
        new CandidateMessage(
            messageType,
            data ?? new object(),
            _messages.Any()
                ? _messages.Last().Metadata.Position + 1
                : 0
        );
}

public record StreamId(Category Category, EntityId EntityId, bool IsCommand);
