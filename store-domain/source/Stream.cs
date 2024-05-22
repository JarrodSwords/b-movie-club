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

    public Result<CandidateMessage> Next(MessageType messageType) =>
        new CandidateMessage(
            messageType,
            _messages.Any() ? _messages.Last().Position + 1 : 0
        );
}
