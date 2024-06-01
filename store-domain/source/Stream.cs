using System.Collections;

namespace Store.Domain;

public class Stream : IEnumerable<Message>
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
        object? data = null
    ) =>
        new CandidateMessage(
            data,
            _messages.Any()
                ? _messages.Last().Metadata.Position + 1
                : 0,
            Id
        );

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<Message> GetEnumerator() => _messages.GetEnumerator();
}

public record StreamId(Category Category, EntityId EntityId, bool IsCommand);
