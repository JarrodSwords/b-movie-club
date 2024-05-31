namespace Store.Domain.Spec.Store;

/// <summary>
/// </summary>
/// <remarks>
///     This integration test should be inherited by any candidate implementations.
/// </remarks>
public abstract partial class WhenPushingAMessage
{
    #region Setup

    private readonly IMessageStore _store;

    protected WhenPushingAMessage()
    {
        _store = CreateMessageStore();
    }

    #endregion

    #region Implementation

    public abstract IMessageStore CreateMessageStore();

    #endregion

    #region Requirements

    [Fact]
    public void ThenMessageIsRetrievable()
    {
        var createFoo = new CandidateMessage(MessageType.Create("CreateFoo"), null, 0);

        _store.Push(createFoo);

        var message = _store.Find(createFoo.MessageId).Value!;

        message.Id.Should().Be(createFoo.MessageId);
    }

    #endregion
}
