﻿namespace Store.Infrastructure.InMemory;

public class InMemoryMessageStore : IMessageStore
{
    private readonly List<Message> _messages = new();

    public Result<Message> Find(MessageId id)
    {
        var message = _messages.Single(x => x.Id == id.Value);

        return new Message(
            message.Id,
            message.Type,
            message.Timestamp,
            message.Position,
            message.GlobalPosition
        );
    }

    public Result Push(CandidateMessage candidateMessage)
    {
        var message = new Message(
            candidateMessage.MessageId,
            candidateMessage.MessageType,
            DateTime.UtcNow,
            0,
            (ulong) _messages.Count
        );

        _messages.Add(message);

        return Success();
    }
}
