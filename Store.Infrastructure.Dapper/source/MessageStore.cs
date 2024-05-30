using Jgs.Errors.Results;
using Store.Domain;
using static Jgs.Errors.Results.Result;

namespace Store.Infrastructure.Dapper;

public class MessageStore : IMessageStore
{
    private readonly List<Message> _messages = new();

    public Result<DomainMessage> Find(MessageId id)
    {
        var message = _messages.Single(x => x.Id == id.Value);

        return new DomainMessage(message.Id, message.Type, message.Timestamp, message.Position);
    }

    public Result Push(CandidateMessage candidateMessage)
    {
        var message = new Message(
            candidateMessage.MessageId,
            candidateMessage.MessageType,
            0
        );

        _messages.Add(message);

        return Success();
    }
}
